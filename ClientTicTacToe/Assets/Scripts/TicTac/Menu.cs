using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    private string p1Name;
    private string p2Name;

    private GameObject Player1Box;
    private GameObject Player2Box;

    private GameObject displayName1;
    private GameObject displayName2;

    private GameObject Player1Overlay;
    private GameObject Player2Overlay;

    private GameObject Player1Input;
    private GameObject Player2Input;

    private GameObject Player1Submit;
    private GameObject Player2Submit;

    private GameObject Player1Ready;
    private GameObject Player2Ready;

    private GameObject Player1Leave;
    private GameObject Player2Leave;

    private GameObject P1Turn;
    private GameObject P2Turn;

    private GameObject GameUI;
    private GameObject GameControl;

    private NetworkManager networkManager;
    private MessageQueue msgQueue;

    private GameObject Join;
    private GameObject JoinButton;

    // Start is called before the first frame update
    void Start()
    {
        Player1Overlay = GameObject.Find("Player1");
        Player2Overlay = GameObject.Find("Player2");

        Player1Box = GameObject.Find("Player1Box");
        Player2Box = GameObject.Find("Player2Box");
        displayName1 = GameObject.Find("Name1");
        displayName2 = GameObject.Find("Name2");

        Player1Input = GameObject.Find("Player1Input");
        Player2Input = GameObject.Find("Player2Input");

        Player1Submit = GameObject.Find("Player1Submit");
        Player2Submit = GameObject.Find("Player2Submit");

        Player1Ready = GameObject.Find("Player1Ready");
        Player2Ready = GameObject.Find("Player2Ready");

        Player1Leave = GameObject.Find("Player1Leave");
        Player2Leave = GameObject.Find("Player2Leave");

        GameUI = GameObject.Find("GameUI");
        GameControl = GameObject.Find("GameControl");
        P1Turn = GameObject.Find("P1Turn");
        P2Turn = GameObject.Find("P2Turn");

        Join = GameObject.Find("Join");
        JoinButton = GameObject.Find("JoinButton");

        GameControl.SetActive(false);


        Join.SetActive(true);
        Player1Overlay.SetActive(false);
        Player2Overlay.SetActive(false);

        networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        msgQueue = networkManager.GetComponent<MessageQueue>();

        msgQueue.AddCallback(Constants.SMSG_LOGIN, OnResponseLogin);
        msgQueue.AddCallback(Constants.SMSG_ENTERNAME, OnResponseEnterName);
        msgQueue.AddCallback(Constants.SMSG_EXIT, OnResponseExit);

        GameUI.SetActive(false);
        P1Turn.SetActive(true);
        P2Turn.SetActive(true);
        displayName1.SetActive(false);
        displayName2.SetActive(false);


    }

    public void OnJoin()
    {
        Debug.Log("Send JoinReq");
        bool connected = networkManager.SendLoginRequest();
        if (!connected)
        {
            Debug.Log("Unable to connect to server");
        }
    }

    public void OnResponseLogin(ExtendedEventArgs eventArgs)
    {
        Debug.Log("Response join");
        ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;
        if (args.status == 0)
        {
            if (args.user_id == 1)
            {
                Join.SetActive(false);
                Player1Overlay.SetActive(true);
                Player2Overlay.SetActive(true);
                Player2Box.SetActive(false);
            }
            else if (args.user_id == 2)
            {
                Join.SetActive(false);
                Player1Overlay.SetActive(true);
                Player1Box.SetActive(false);
                Player2Overlay.SetActive(true);
            }
            else
            {
                Debug.Log("ERROR: Invalid user_id in ResponseJoin: " + args.user_id);
                return;
            }
            Constants.USER_ID = args.user_id;
            Debug.Log("MYUSERID: " + Constants.USER_ID);
            Constants.OP_ID = 3 - args.user_id;
            if (args.op_id > 0)
            {
                if (args.op_id == Constants.OP_ID)
                {
                    if (args.user_id == 1)
                    {
                        displayName2.SetActive(true);
                        displayName2.GetComponent<Text>().text = args.op_name;
                    }
                    else
                    {
                        displayName1.SetActive(true);
                        displayName1.GetComponent<Text>().text = args.op_name;
                    }
                }
            }
        }
    }


    public void OnPlayer1Submit()
    {
        
        string p1Name = GameObject.Find("Player1Input").GetComponent<InputField>().text;

        Debug.Log(p1Name);
        if(p1Name.Length < 1)
        {
            displayName1.GetComponent<Text>().text = "Player 1";
        }
        else
        {
            displayName1.GetComponent<Text>().text = p1Name;
        }
        Player1Box.SetActive(false);
        displayName1.SetActive(true);
        Debug.Log("Send SetNameReq: " + p1Name);
        networkManager.SendEnterNameRequest(p1Name);
    }

    public void OnResponseEnterName(ExtendedEventArgs eventArgs)
    {
        Debug.Log("Response Set Name");
        
        Debug.Log(Constants.USER_ID);
        ResponseEnterNameEventArgs args = eventArgs as ResponseEnterNameEventArgs;
        Debug.Log(args.user_id);
        if (args.user_id != Constants.USER_ID)
        {
            if (args.user_id == 1)
            {
                displayName1.SetActive(true);
                displayName1.GetComponent<Text>().text = args.name;
            }
            else
            {
                displayName2.SetActive(true);
                displayName2.GetComponent<Text>().text = args.name;
            }
        }
    }

    public void OnPlayer2Submit()
    {
        string p2Name = GameObject.Find("Player2Input").GetComponent<InputField>().text;

        Debug.Log(p2Name);
        if (p2Name.Length < 1)
        {
            displayName2.GetComponent<Text>().text = "Player 2";
        }
        else
        {
            displayName2.GetComponent<Text>().text = p2Name;
        }
        Player2Box.SetActive(false);
        displayName2.SetActive(true);
        Debug.Log("Send SetNameReq: " + p2Name);
        networkManager.SendEnterNameRequest(p2Name);
    }

    public void OnLeave()
    {
        Debug.Log("Send LeaveReq");
        networkManager.SendExitRequest();
        Join.SetActive(true);
        if (Constants.USER_ID == 1)
        {
            Player1Input.SetActive(true);
            Player1Submit.SetActive(true);
            displayName1.SetActive(false);
            displayName1.GetComponent<Text>().text = "";
        }
        else
        {
            Player2Input.SetActive(true);
            Player2Submit.SetActive(true);
            displayName2.SetActive(false);
            displayName2.GetComponent<Text>().text = "";
        }
        Player1Overlay.SetActive(false);
        Player2Overlay.SetActive(false);
    }

    public void OnResponseExit(ExtendedEventArgs eventArgs)
    {
        ResponseExitEventArgs args = eventArgs as ResponseExitEventArgs;
        if (args.user_id != Constants.USER_ID)
        {
            if (args.user_id != Constants.USER_ID)
            {
                if (args.user_id == 1)
                {
                    displayName1.SetActive(false);
                    displayName1.GetComponent<Text>().text = "";
                }
                else
                {
                    displayName2.SetActive(false);
                    displayName2.GetComponent<Text>().text = "";
                }
            }
        }
    }

    void setGameUI()
    {
        Player1Overlay.SetActive(false);
        Player2Overlay.SetActive(false);
        GameUI.SetActive(true);
        P1Turn.GetComponent<Text>().text = displayName1.GetComponent<Text>().text + "'s Turn";
        P2Turn.GetComponent<Text>().text = displayName2.GetComponent<Text>().text + "'s Turn";
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
