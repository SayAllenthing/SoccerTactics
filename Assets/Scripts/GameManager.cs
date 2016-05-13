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

	//Move UI
	public Text TurnText;
	public Text ActionsText;
	public CanvasGroup PlayerCanvas;
	public Text NameText;
	public Text ShootingText;
	public Text PassingText;
	public Text DefenseText;

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
			SetSelectedPlayer(null);
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
		{
			TheField.SelectTile(t);
		}
		else
		{			
			if(t.bIsNetTile && SelectedPlayer.HasBall())
			{
				//Do shot
				Ball.TheBall.Shoot(t);
			}
			else if(t.HasPlayerOnTeam(CurrentTurn) && SelectedPlayer.HasBall())
			{
				//Do pass
                if (GameActionResolver.Instance.ResolvePass(SelectedPlayer, t.GetPlayer(CurrentTurn)))
                    Ball.TheBall.Pass(t.GetPlayer(CurrentTurn));
                else
                    Ball.TheBall.PassFail(GameActionResolver.Instance.FailedTile);
			}
			else
			{
				//Move
				if(!SelectedPlayer.IsLegalMove(t.X, t.Y))
				{
					return;
				}

				SelectedPlayer.Move(t.X, t.Y);
			}

			SelectedPlayer = null;
			TheField.DeselectTiles();
			OnTurnEnd();
		}
	}

	public void SetSelectedPlayer(Player p)
	{
		SelectedPlayer = p;

		DisplayPlayerStats();
	}

	void DisplayPlayerStats()
	{
		if(SelectedPlayer == null)
		{
			PlayerCanvas.alpha = 0;
			return;
		}

		PlayerStats ps = SelectedPlayer.GetStats();

		NameText.text = ps.Name;
		ShootingText.text = ps.coreStats.Shooting.ToString();
		PassingText.text = ps.coreStats.Passing.ToString();
		DefenseText.text = ps.coreStats.Tackling.ToString();

		PlayerCanvas.alpha = 1;
	}

	void OnTurnEnd()
	{
		CurrentActions--;

		if(CurrentActions <= 0)
		{
			if(CurrentTurn == "Home")
				CurrentTurn = "Away";
			else
				CurrentTurn = "Home";

			CurrentActions = 2;
		}

		TurnText.text = "Current Turn: " + CurrentTurn;
		ActionsText.text = "Actions: " + CurrentActions;
	}   
}
