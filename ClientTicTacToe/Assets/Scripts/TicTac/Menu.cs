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

    private GameObject GameControl;

    // Start is called before the first frame update
    void Start()
    {
        Player1Box = GameObject.Find("Player1Box");
        Player2Box = GameObject.Find("Player2Box");
        displayName1 = GameObject.Find("Name1");
        displayName2 = GameObject.Find("Name2");

        Player1Box.SetActive(true);
        Player2Box.SetActive(true);

        displayName1.SetActive(false);
        displayName2.SetActive(false);


    }

    public void OnPlayer1Submit()
    {
        string p1Name = GameObject.Find("Player1Input").GetComponent<InputField>().text;

        Debug.Log(p1Name);
        displayName1.GetComponent<Text>().text = p1Name;
        Player1Box.SetActive(false);
        displayName1.SetActive(true);
        if (displayName2.activeSelf == true)
        {
            
        }
    }

    public void OnPlayer2Submit()
    {
        string p2Name = GameObject.Find("Player2Input").GetComponent<InputField>().text;

        Debug.Log(p2Name);
        displayName2.GetComponent<Text>().text = p2Name;
        Player2Box.SetActive(false);
        displayName2.SetActive(true);
        if (displayName1.activeSelf == true)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
