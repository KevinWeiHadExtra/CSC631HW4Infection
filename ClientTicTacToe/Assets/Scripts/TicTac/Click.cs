using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Material[] Material;
    public int player = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.name != null)
                {
                    PlayerColor(hit.transform.gameObject);
                }
            }
        }
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


}
