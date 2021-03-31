using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player[] Players = new Player[2];
	public GameObject TilePrefab;

	private Tile[,] gameBoard = new Tile[6,5];

	private int currentPlayer = 1;
	private bool canInteract = false;
	private bool choosingInteraction = false;

	private bool useNetwork;
	private NetworkManager networkManager;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
		MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
		msgQueue.AddCallback(Constants.SMSG_INTERACT, OnResponseInteract);
	}

	public Player GetCurrentPlayer()
	{
		return Players[currentPlayer - 1];
	}

	public void Init(Player player1, Player player2)
	{
		Players[0] = player1;
		Players[1] = player2;
		currentPlayer = 1;
		useNetwork = (!player1.IsMouseControlled || !player2.IsMouseControlled);
	}

	public void CreateTiles()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject tileObj1 = Instantiate(TilePrefab, new Vector3(0, 0, (float)i), Quaternion.identity);
			tileObj1.GetComponentInChildren<Renderer>().material.color = Players[0].Color;
			Tile tile1 = tileObj1.GetComponent<Tile>();
			tile1.Index = i;
			Players[0].AddTile(tile1);
			gameBoard[0, i] = tile1;
			GameObject tileObj2 = Instantiate(TilePrefab, new Vector3(5, 0, (float)i), Quaternion.identity);
			tileObj2.GetComponentInChildren<Renderer>().material.color = Players[1].Color;
			Tile tile2 = tileObj2.GetComponent<Tile>();
			tile2.Index = i;
			gameBoard[5, i] = tile2;
			Players[1].AddTile(tile2);
		}
	}

	public bool CanInteract()
	{
		return canInteract;
	}

	public void StartInteraction()
	{
		if (canInteract)
		{
			choosingInteraction = true;
		}
	}

	public void EndInteraction(Tile tile)
	{
		EndTurn();
	}

	public void EndInteractedWith(Tile tile)
	{
		// Do nothing
	}

	public void EndMove(Tile tile)
	{
		bool tileCanInteract = false;
		int[] deltaX = { 1, 0, -1, 0 };
		int[] deltaY = { 0, 1, 0, -1 };
		for (int i = 0; i < 4; ++i)
		{
			int x = tile.x + deltaX[i];
			int y = tile.y + deltaY[i];
			if (x >= 0 && x < 6 && y >= 0 && y < 5)
			{
				if (gameBoard[x, y] && gameBoard[x, y].Owner != tile.Owner)
				{
					tileCanInteract = true;
					break;
				}
			}
		}
		if (tile.Owner.IsMouseControlled)
		{
			canInteract = tileCanInteract;
		}

		if (!tileCanInteract)
		{
			EndTurn();
		}
	}

	public void EndTurn()
	{
		ObjectSelector.SetSelectedObject(null);
		canInteract = false;
		currentPlayer = 3 - currentPlayer;
	}

	public void ProcessClick(GameObject hitObject)
	{
		if (hitObject.tag == "Tile")
		{
			if (ObjectSelector.SelectedObject)
			{
				Tile tile = ObjectSelector.SelectedObject.GetComponentInParent<Tile>();
				if (tile)
				{
					int x = (int)hitObject.transform.position.x;
					int y = (int)hitObject.transform.position.z;
					if (gameBoard[x, y] == null)
					{
						if (tile.CanMoveTo(x, y))
						{
							if (useNetwork)
							{
								networkManager.SendMoveRequest(tile.Index, x, y);
							}
							gameBoard[tile.x, tile.y] = null;
							tile.Move(x, y);
							gameBoard[x, y] = tile;
						}
					}
				}
			}
		}
		else
		{
			Tile tile = hitObject.GetComponentInParent<Tile>();
			if (tile)
			{
				if (choosingInteraction)
				{
					Tile selectedTile = ObjectSelector.SelectedObject?.GetComponentInParent<Tile>();
					if (selectedTile)
					{
						if (AreNeighbors(tile, selectedTile) && tile.Owner != selectedTile.Owner)
						{
							if (useNetwork)
							{
								networkManager.SendInteractRequest(selectedTile.Index, tile.Index);
							}
							selectedTile.Interact(tile);
							choosingInteraction = false;
						}
					}
				}
				else if (tile.gameObject == ObjectSelector.SelectedObject)
				{
					ObjectSelector.SetSelectedObject(null);
				}
				else if (tile.Owner.IsMouseControlled && tile.Owner == Players[currentPlayer - 1])
				{
					ObjectSelector.SetSelectedObject(hitObject);
				}
			}
		}
	}

	public bool HighlightEnabled(GameObject gameObject)
	{
		if (gameObject.tag == "Tile")
		{
			Tile tile = ObjectSelector.SelectedObject?.GetComponentInParent<Tile>();
			if (tile)
			{
				int x = (int)gameObject.transform.position.x;
				int y = (int)gameObject.transform.position.z;
				return (gameBoard[x, y] == null);
			}
		}
		else if (choosingInteraction)
		{
			Tile tile = gameObject.GetComponentInParent<Tile>();
			Tile selectedTile = ObjectSelector.SelectedObject?.GetComponentInParent<Tile>();
			if (tile && selectedTile)
			{
				return AreNeighbors(tile, selectedTile) && tile.Owner != selectedTile.Owner;
			}
			else
			{
				return false;
			}
		}
		else
		{
			Tile tile = gameObject.GetComponentInParent<Tile>();
			if (tile)
			{
				return (tile.Owner.IsMouseControlled && tile.Owner == Players[currentPlayer - 1]);
			}
		}
		return true;
	}

	private bool AreNeighbors(Tile tile1, Tile tile2)
	{
		return (Math.Abs(tile1.x - tile2.x) + Math.Abs(tile1.y - tile2.y) == 1);
	}

	public void OnResponseMove(ExtendedEventArgs eventArgs)
	{
		ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
		if (args.user_id == Constants.OP_ID)
		{
			int pieceIndex = args.piece_idx;
			int x = args.x;
			int y = args.y;
			Tile tile = Players[args.user_id - 1].Tiles[pieceIndex];
			gameBoard[tile.x, tile.y] = null;
			tile.Move(x, y);
			gameBoard[x, y] = tile;
		}
		else if (args.user_id == Constants.USER_ID)
		{
			// Ignore
		}
		else
		{
			Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		}
	}

	public void OnResponseInteract(ExtendedEventArgs eventArgs)
	{
		ResponseInteractEventArgs args = eventArgs as ResponseInteractEventArgs;
		if (args.user_id == Constants.OP_ID)
		{
			int pieceIndex = args.piece_idx;
			int targetIndex = args.target_idx;
			Tile tile = Players[args.user_id - 1].Tiles[pieceIndex];
			Tile target = Players[Constants.USER_ID - 1].Tiles[targetIndex];
			tile.Interact(target);
		}
		else if (args.user_id == Constants.USER_ID)
		{
			// Ignore
		}
		else
		{
			Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		}
	}
}
