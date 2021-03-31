public class Constants
{
	// Constants
	public static readonly string CLIENT_VERSION = "1.00";
	public static readonly string REMOTE_HOST = "localhost";
	public static readonly int REMOTE_PORT = 1729;
	
	// Request (1xx) + Response (2xx)
	public static readonly short CMSG_LOGIN = 101;
	public static readonly short SMSG_LOGIN = 201;
	public static readonly short CMSG_EXIT = 102;
	public static readonly short SMSG_EXIT = 202;
	public static readonly short CMSG_ENTERNAME = 103;
	public static readonly short SMSG_ENTERNAME = 203;
	public static readonly short CMSG_CONFIRM = 104;
	public static readonly short SMSG_CONFIRM = 204;
	public static readonly short CMSG_MOVE = 105;
	public static readonly short SMSG_MOVE = 205;
	public static readonly short CMSG_INTERACT = 106;
	public static readonly short SMSG_INTERACT = 206;
	public static readonly short CMSG_HEARTBEAT = 111;

	public static int USER_ID = -1;
	public static int OP_ID = -1;
}