using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseEnterNameEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public string name { get; set; } // Their new name

	public ResponseEnterNameEventArgs()
	{
		event_id = Constants.SMSG_ENTERNAME;
	}
}

public class ResponseEnterName : NetworkResponse
{
	private int user_id;
	private string name;

	public ResponseEnterName()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		name = DataReader.ReadString(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseEnterNameEventArgs args = new ResponseEnterNameEventArgs
		{
			user_id = user_id,
			name = name
		};

		return args;
	}
}
