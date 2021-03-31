using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLogin : NetworkRequest
{
	public RequestLogin()
	{
		request_id = Constants.CMSG_LOGIN;
	}

	public void send()
	{
		packet = new GamePacket(request_id);
	}
}
