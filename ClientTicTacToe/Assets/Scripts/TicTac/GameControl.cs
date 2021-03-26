using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        Player1.SetActive(false);
        Player2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
