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

    private GameObject P1Turn;
    private GameObject P2Turn;

    private GameObject GameUI;
    private GameObject GameControl;

    // Start is called before the first frame update
    void Start()
    {
        Player1Overlay = GameObject.Find("Player1");
        Player2Overlay = GameObject.Find("Player2");

        Player1Box = GameObject.Find("Player1Box");
        Player2Box = GameObject.Find("Player2Box");
        displayName1 = GameObject.Find("Name1");
        displayName2 = GameObject.Find("Name2");

        GameUI = GameObject.Find("GameUI");
        GameControl = GameObject.Find("GameControl");
        P1Turn = GameObject.Find("P1Turn");
        P2Turn = GameObject.Find("P2Turn");

        GameControl.SetActive(false);

        Player1Box.SetActive(true);
        Player2Box.SetActive(true);

        GameUI.SetActive(false);
        P1Turn.SetActive(true);
        P2Turn.SetActive(true);
        displayName1.SetActive(false);
        displayName2.SetActive(false);


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
        if (displayName2.activeSelf == true)
        {
            setGameUI();
            GameControl.SetActive(true);
            GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
            gameControl.start();
            
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
        if (displayName1.activeSelf == true)
        {
            setGameUI();
            GameControl.SetActive(true);
            GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
            gameControl.start();
            
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
