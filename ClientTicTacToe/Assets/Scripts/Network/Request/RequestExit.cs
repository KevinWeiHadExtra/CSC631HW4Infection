using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestExit : NetworkRequest
{
	public RequestExit()
	{
		request_id = Constants.CMSG_EXIT;
	}

	public void send()
	{
		packet = new GamePacket(request_id);
	}
}
