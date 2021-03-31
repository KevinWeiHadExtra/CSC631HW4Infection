using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseExitEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request

	public ResponseExitEventArgs()
	{
		event_id = Constants.SMSG_EXIT;
	}
}

public class ResponseLeave : NetworkResponse
{
	private int user_id;

	public ResponseLeave()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseExitEventArgs args = new ResponseExitEventArgs
		{
			user_id = user_id
		};

		return args;
	}
}
