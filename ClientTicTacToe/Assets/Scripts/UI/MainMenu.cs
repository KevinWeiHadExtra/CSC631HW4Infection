using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private GameObject rootMenuPanel;
	private GameObject hotseatMenuPanel;
	private GameObject networkMenuPanel;

	private GameObject messageBox;
	private TMPro.TextMeshProUGUI messageBoxMsg;

	private TMPro.TextMeshProUGUI player1Name;
	private TMPro.TextMeshProUGUI player2Name;
	private GameObject player1Input;
	private GameObject player2Input;

	private TMPro.TextMeshProUGUI playerName;
	private TMPro.TextMeshProUGUI opponentName;
	private GameObject playerInput;
	private GameObject opponentInput;

	private NetworkManager networkManager;
	private MessageQueue msgQueue;

	private string p1Name = "Player 1";
	private string p2Name = "Player 2";

	private bool ready = false;
	private bool opReady = false;

    // Start is called before the first frame update
    void Start()
    {
		rootMenuPanel = GameObject.Find("Root Menu");
		hotseatMenuPanel = GameObject.Find("Hotseat Menu");
		networkMenuPanel = GameObject.Find("Network Menu");

		messageBox = GameObject.Find("Message Box");
		messageBoxMsg = messageBox.transform.Find("Message").gameObject.GetComponent<TMPro.TextMeshProUGUI>();

		player1Name = GameObject.Find("Player1Name").GetComponent<TMPro.TextMeshProUGUI>();
		player2Name = GameObject.Find("Player2Name").GetComponent<TMPro.TextMeshProUGUI>();
		player1Input = GameObject.Find("NetPlayer1Input");
		player2Input = GameObject.Find("NetPlayer2Input");

		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
		msgQueue = networkManager.GetComponent<MessageQueue>();

		msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
		msgQueue.AddCallback(Constants.SMSG_LEAVE, OnResponseLeave);
		msgQueue.AddCallback(Constants.SMSG_SETNAME, OnResponseSetName);
		msgQueue.AddCallback(Constants.SMSG_READY, OnResponseReady);

		rootMenuPanel.SetActive(true);
		hotseatMenuPanel.SetActive(false);
		networkMenuPanel.SetActive(false);
		messageBox.SetActive(false);
	}

	#region RootMenu
	public void OnHotseatClick()
	{
		rootMenuPanel.SetActive(false);
		hotseatMenuPanel.SetActive(true);
	}

	public void OnNetworkClick()
	{
		Debug.Log("Send JoinReq");
		bool connected = networkManager.SendJoinRequest();
		Debug.Log("Send LoginReq");
		bool connected = networkManager.SendLoginRequest();
		if (!connected)
		{
			messageBoxMsg.text = "Unable to connect to server.";
			messageBox.SetActive(true);
		}
	}

	public void OnExitClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
	#endregion

	#region HotseatMenu
	public void OnStartClick()
	{
		StartHotseatGame();
	}

	public void OnBackClick()
	{
		rootMenuPanel.SetActive(true);
		hotseatMenuPanel.SetActive(false);
		networkMenuPanel.SetActive(false);
		messageBox.SetActive(false);
	}
	#endregion

	#region NetworkMenu
	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	public void OnResponseLogin(ExtendedEventArgs eventArgs)
	{
		ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;
		ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;
		if (args.status == 0)
		{
			if (args.user_id == 1)
			{
				playerName = player1Name;
				opponentName = player2Name;
				playerInput = player1Input;
				opponentInput = player2Input;
			}
			else if (args.user_id == 2)
			{
				playerName = player2Name;
				opponentName = player1Name;
				playerInput = player2Input;
				opponentInput = player1Input;
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseJoin: " + args.user_id);
				Debug.Log("ERROR: Invalid user_id in ResponseLogin: " + args.user_id);
				messageBoxMsg.text = "Error joining game. Network returned invalid response.";
				messageBox.SetActive(true);
				return;
			}
			Constants.USER_ID = args.user_id;
			Constants.OP_ID = 3 - args.user_id;

			if (args.op_id > 0)
			{
				if (args.op_id == Constants.OP_ID)
				{
					opponentName.text = args.op_name;
					opReady = args.op_ready;
					opConfirm = args.op_confirm;
				}
				else
				{
					Debug.Log("ERROR: Invalid op_id in ResponseJoin: " + args.op_id);
					Debug.Log("ERROR: Invalid op_id in ResponseLogin: " + args.op_id);
					messageBoxMsg.text = "Error joining game. Network returned invalid response.";
					messageBox.SetActive(true);
					return;
				}
			}
			else
			{
				opponentName.text = "Waiting for opponent";
			}

			playerInput.SetActive(true);
			opponentName.gameObject.SetActive(true);
			playerName.gameObject.SetActive(false);
			opponentInput.SetActive(false);

			rootMenuPanel.SetActive(false);
			networkMenuPanel.SetActive(true);
		}
		else
		{
			messageBoxMsg.text = "Server is full.";
			messageBox.SetActive(true);
		}
	}

	public void OnLeave()
	public void OnExit()
	{
		Debug.Log("Send LeaveReq");
		networkManager.SendLeaveRequest();
		networkManager.SendExitRequest();
		rootMenuPanel.SetActive(true);
		networkMenuPanel.SetActive(false);
		ready = false;
		confirm = false;
	}

	public void OnResponseLeave(ExtendedEventArgs eventArgs)
	public void OnResponseExit(ExtendedEventArgs eventArgs)
	{
		ResponseLeaveEventArgs args = eventArgs as ResponseLeaveEventArgs;
		ResponseExitEventArgs args = eventArgs as ResponseExitEventArgs;
		if (args.user_id != Constants.USER_ID)
		{
			opponentName.text = "Waiting for opponent";
			opReady = false;
			opConfirm = false;
		}
	}

	public void OnPlayerNameSet(string name)
	public void OnPlayerNameEnter(string name)
	{
		Debug.Log("Send SetNameReq: " + name);
		networkManager.SendSetNameRequest(name);
		networkManager.SendEnterNameRequest(name);
		if (Constants.USER_ID == 1)
		{
			p1Name = name;
		}
		else
		{
			p2Name = name;
		}
	}

	public void OnResponseSetName(ExtendedEventArgs eventArgs)
	public void OnResponseEnterName(ExtendedEventArgs eventArgs)
	{
		ResponseSetNameEventArgs args = eventArgs as ResponseSetNameEventArgs;
		ResponseEnterNameEventArgs args = eventArgs as ResponseEnterNameEventArgs;
		if (args.user_id != Constants.USER_ID)
		{
			opponentName.text = args.name;
			if (args.user_id == 1)
			{
				p1Name = args.name;
			}
			else
			{
				p2Name = args.name;
			}
		}
	}

	public void OnReadyClick()
	public void OnConfirmClick()
	{
		Debug.Log("Send ReadyReq");
		networkManager.SendReadyRequest();
		Debug.Log("Send ConfirmReq");
		networkManager.SendConfirmRequest();
	}

	public void OnResponseReady(ExtendedEventArgs eventArgs)
	public void OnResponseConfirm(ExtendedEventArgs eventArgs)
	{
		ResponseReadyEventArgs args = eventArgs as ResponseReadyEventArgs;
		if (Constants.USER_ID == -1) // Haven't joined, but got ready message
		ResponseConfirmEventArgs args = eventArgs as ResponseConfirmEventArgs;
		if (Constants.USER_ID == -1) // Haven't joined, but got confirm message
		{
			opReady = true;
			opConfirm = true;
		}
		else
		{
			if (args.user_id == Constants.OP_ID)
			{
				opReady = true;
				opConfirm = true;
			}
			else if (args.user_id == Constants.USER_ID)
			{
				ready = true;
				confirm = true;
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
				Debug.Log("ERROR: Invalid user_id in ResponseConfirm: " + args.user_id);
				messageBoxMsg.text = "Error starting game. Network returned invalid response.";
				messageBox.SetActive(true);
				return;
			}
		}

		if (ready && opReady)
		if (confirm && opConfirm)
		{
			StartNetworkGame();
		}
	}
	#endregion

	public void OnOKClick()
	{
		messageBox.SetActive(false);
	}

	private void StartHotseatGame()
	{
		GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		string p1Name = GameObject.Find("HotPlayer1Name").GetComponent<TMPro.TextMeshProUGUI>().text;
		if (p1Name.Length == 1)
		{
			p1Name = "Player 1";
		}
		string p2Name = GameObject.Find("HotPlayer2Name").GetComponent<TMPro.TextMeshProUGUI>().text;
		if (p2Name.Length == 1)
		{
			p2Name = "Player 2";
		}
		Player player1 = new Player(1, p1Name, new Color(0.9f, 0.1f, 0.1f), true);
		Player player2 = new Player(2, p2Name, new Color(0.2f, 0.2f, 1.0f), true);
		gameManager.Init(player1, player2);
		SceneManager.LoadScene("Game");
	}

	private void StartNetworkGame()
	{
		GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		if (p1Name.Length == 0)
		{
			p1Name = "Player 1";
		}
		if (p2Name.Length == 0)
		{
			p2Name = "Player 2";
		}
		Player player1 = new Player(1, p1Name, new Color(0.9f, 0.1f, 0.1f), Constants.USER_ID == 1);
		Player player2 = new Player(2, p2Name, new Color(0.2f, 0.2f, 1.0f), Constants.USER_ID == 2);
		gameManager.Init(player1, player2);
		SceneManager.LoadScene("Game");
	}
}
