using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    LayerMask Mask = (1 << 8);
    public Field TheField;

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TheField.DeselectTiles();
        }
	}

    void OnClick(int button)
    {
        Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 50, Mask))
        {
            GameObject g = hit.collider.gameObject;

            if (g.tag == "Tile")
            {
                TheField.SelectTile(g.GetComponent<Tile>());
            }
        }
    }

   
}
