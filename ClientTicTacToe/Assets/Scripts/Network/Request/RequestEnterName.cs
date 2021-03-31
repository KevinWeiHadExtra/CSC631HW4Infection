using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestEnterName : NetworkRequest
{
	public RequestEnterName()
	{
		request_id = Constants.CMSG_ENTERNAME;
	}

	public void send(string name)
	{
		packet = new GamePacket(request_id);
		packet.addString(name);
	}
}
