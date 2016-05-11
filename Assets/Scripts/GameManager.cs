using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    LayerMask Mask = (1 << 8);
    public Field TheField;

	public static GameManager Instance;

	public Player SelectedPlayer = null;

	public string CurrentTurn = "Home";
	int CurrentActions = 2;
	public Text TurnText;

	void Start()
	{
		if(Instance != null)
			Instance = null;

		Instance = this;
	}

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
				Tile t = g.GetComponent<Tile>();

				OnClickTile(t);
            }
        }
    }

	void OnClickTile(Tile t)
	{
		if(SelectedPlayer == null)
			TheField.SelectTile(t);
		else
		{
			if(t.bIsNetTile)
			{
				//Do shot
			}
			else if(t.HasPlayerOnTeam(CurrentTurn))
			{
				//Do pass
			}
			else
			{
				SelectedPlayer.Move(t.X, t.Y);
				SelectedPlayer = null;
				TheField.DeselectTiles();
				OnTurnEnd();
			}
		}
	}

	void OnTurnEnd()
	{
		CurrentActions--;
		if(CurrentActions > 0)
			return;

		if(CurrentTurn == "Home")
			CurrentTurn = "Away";
		else
			CurrentTurn = "Home";

		TurnText.text = "Current Turn: " + CurrentTurn;

		CurrentActions = 2;
	}

   
}
