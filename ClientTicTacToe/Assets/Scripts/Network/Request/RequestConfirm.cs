using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestConfirm : NetworkRequest
{
	public RequestConfirm()
	{
		request_id = Constants.CMSG_CONFIRM;
	}

	public void send()
	{
		packet = new GamePacket(request_id);
	}
}
