using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	private ConnectionManager cManager;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		gameObject.AddComponent<MessageQueue>();
		gameObject.AddComponent<ConnectionManager>();

		NetworkRequestTable.init();
		NetworkResponseTable.init();
	}

	// Start is called before the first frame update
	void Start()
    {
		cManager = GetComponent<ConnectionManager>();

		if (cManager)
		{
			cManager.setupSocket();

			StartCoroutine(RequestHeartbeat(0.1f));
		}
	}

	public bool SendLoginRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestLogin request = new RequestLogin();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendExitRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestExit request = new RequestExit();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendEnterNameRequest(string Name)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestEnterName request = new RequestEnterName();
			request.send(Name);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendConfirmRequest()
	{
		if (cManager && cManager.IsConnected())
		{
			RequestConfirm request = new RequestConfirm();
			request.send();
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendMoveRequest(int pieceIndex, int x, int y)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestMove request = new RequestMove();
			request.send(pieceIndex, x, y);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public bool SendInteractRequest(int pieceIndex, int targetIndex)
	{
		if (cManager && cManager.IsConnected())
		{
			RequestInteract request = new RequestInteract();
			request.send(pieceIndex, targetIndex);
			cManager.send(request);
			return true;
		}
		return false;
	}

	public IEnumerator RequestHeartbeat(float time)
	{
		yield return new WaitForSeconds(time);

		if (cManager)
		{
			RequestHeartbeat request = new RequestHeartbeat();
			request.send();
			cManager.send(request);
		}

		StartCoroutine(RequestHeartbeat(time));
	}
}
