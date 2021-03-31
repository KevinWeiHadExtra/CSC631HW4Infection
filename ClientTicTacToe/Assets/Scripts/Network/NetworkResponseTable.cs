using UnityEngine;

using System;
using System.Collections.Generic;

public class NetworkResponseTable {

	public static Dictionary<short, Type> responseTable { get; set; }
	
	public static void init() {
		responseTable = new Dictionary<short, Type>();
		add(Constants.SMSG_LOGIN, "ResponseLogin");
		add(Constants.SMSG_EXIT, "ResponseExit");
		add(Constants.SMSG_ENTERNAME, "ResponseEnterName");
		add(Constants.SMSG_CONFIRM, "ResponseCofirm");
		add(Constants.SMSG_MOVE, "ResponseMove");
		add(Constants.SMSG_INTERACT, "ResponseInteract");
	}
	
	public static void add(short response_id, string name) {
		responseTable.Add(response_id, Type.GetType(name));
	}
	
	public static NetworkResponse get(short response_id) {
		init ();
		NetworkResponse response = null;
		if (responseTable.ContainsKey(response_id)) {
			response = (NetworkResponse) Activator.CreateInstance(responseTable[response_id]);
			response.response_id = response_id;
		} else {
			Debug.Log("Response [" + response_id + "] Not Found");
		}
		
		return response;
	}
}
