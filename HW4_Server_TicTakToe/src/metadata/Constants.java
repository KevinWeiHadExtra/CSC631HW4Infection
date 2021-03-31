package metadata;

/**
 * The Constants class stores important variables as constants for later use.
 */
public class Constants {
    // Constants
	final public static String CLIENT_VERSION = "1.00";
	final public static String REMOTE_HOST = "localhost";
    final public static int REMOTE_PORT = 9252;
    final public static int TIMEOUT_SECONDS = 90;
    
    // Request (1xx) + Response (2xx)
	final public static short CMSG_LOGIN = 101;
	final public static short SMSG_LOGIN = 201;
	final public static short CMSG_EXIT = 102;
	final public static short SMSG_EXIT = 202;
	final public static short CMSG_ENTERNAME = 103;
	final public static short SMSG_ENTERNAME = 203;
	final public static short CMSG_CONFIRM = 104;
	final public static short SMSG_CONFIRM = 204;
	final public static short CMSG_MOVE = 105;
	final public static short SMSG_MOVE = 205;
	final public static short CMSG_INTERACT = 106;
	final public static short SMSG_INTERACT = 206;

	final public static short CMSG_MONITOR = 111;

	final public static int USER_ID = -1;
	final public static int OP_ID = -1;
}
