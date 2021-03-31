using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreator : MonoBehaviour
{
	private static GameManager gameManager;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		gameManager.CreateTiles();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
