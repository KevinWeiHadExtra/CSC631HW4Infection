using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseLoginEventArgs : ExtendedEventArgs
{
	public short status { get; set; } // 0 = success
	public int user_id { get; set; } // 1 is first to join, 2 is second, anything else is not valid!
	public int op_id { get; set; } // opposite of user_id out of 1 and 2, or 0 if no other player has joined
	public string op_name { get; set; } // opponent name, if known
	public bool op_confirm { get; set; } // has opponent clicked confirm?

	public ResponseLoginEventArgs()
	{
		event_id = Constants.SMSG_LOGIN;
	}
}

public class ResponseLogin : NetworkResponse
{
	private short status;
	private int user_id;
	private int op_id;
	private string op_name;
	private bool op_confirm;

	public ResponseLogin()
	{
	}

	public override void parse()
	{
		status = DataReader.ReadShort(dataStream);
		if (status == 0)
		{
			user_id = DataReader.ReadInt(dataStream);
			op_id = DataReader.ReadInt(dataStream);
			op_name = DataReader.ReadString(dataStream);
			op_confirm = DataReader.ReadBool(dataStream);
		}
	}

	public override ExtendedEventArgs process()
	{
		ResponseLoginEventArgs args = new ResponseLoginEventArgs
		{
			status = status
		};
		if (status == 0)
		{
			args.user_id = user_id;
			args.op_id = op_id;
			args.op_name = op_name;
			args.op_confirm = op_confirm;
		}

		return args;
	}
}
