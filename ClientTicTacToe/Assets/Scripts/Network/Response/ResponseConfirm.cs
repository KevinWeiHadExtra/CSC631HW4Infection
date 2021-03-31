using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseConfirmEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request

	public ResponseConfirmEventArgs()
	{
		event_id = Constants.SMSG_CONFIRM;
	}
}

public class ResponseConfirm : NetworkResponse
{
	private int user_id;

	public ResponseConfirm()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseConfirmEventArgs args = new ResponseConfirmEventArgs
		{
			user_id = user_id
		};

		return args;
	}
}
