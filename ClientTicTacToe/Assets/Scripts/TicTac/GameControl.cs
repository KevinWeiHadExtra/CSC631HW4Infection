using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    private GameObject GameUI;
    private GameObject P1Turn;
    private GameObject P2Turn;
    private GameObject GameOver;

    public Material[] Material;
    private int player = 1;
    private int turn = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (isGameBoard(hit.transform.name))
                {
                    PlayerColor(hit.transform.gameObject);
                    turn++;
                }
            }
        }
        if (turn == 9)
        {
            Debug.Log("Game Over");
            P1Turn.SetActive(false);
            P2Turn.SetActive(false);
            GameOver.SetActive(true);
            enabled = false;
        }
        else if(turn % 2 == 0)
        {
            P1Turn.SetActive(true);
            P2Turn.SetActive(false);
        }
        else
        {
            P1Turn.SetActive(false);
            P2Turn.SetActive(true);
        }
    }

    public void start()
    {
        GameUI = GameObject.Find("GameUI");
        P1Turn = GameObject.Find("P1Turn");
        P2Turn = GameObject.Find("P2Turn");
        GameOver = GameObject.Find("GameOver");
        GameOver.SetActive(false);
        P1Turn.SetActive(true);
        P2Turn.SetActive(false);
        Debug.Log("Game Start");
        gamePlay();
    }

    private void gamePlay()
    {

    }
    
    private void PlayerColor(GameObject obj)
    {
        print(obj.name);
        if (player == 1)
        {
            obj.GetComponent<MeshRenderer>().material = Material[0];
            player++;
        }
        else
        {
            obj.GetComponent<MeshRenderer>().material = Material[1];
            player--;
        }

    }

    private bool isGameBoard(string obj)
    {
        if (obj == "Block1" || obj == "Block2" || obj == "Block3" || obj == "Block4" || obj == "Block5" || obj == "Block6" || obj == "Block7" || obj == "Block8" || obj == "Block9") { return true; }
        else { return false; }
    }

}
