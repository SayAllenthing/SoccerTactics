using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public int X;
	public int Y;

    public Material MatNormal;
    public Material MatSelected;
	public Material MatNet;

	public List<Player> Players = new List<Player>();

	public bool bIsNetTile = false;

    public void SetPosition(int x, int y)
    {
		X = x;
		Y = y;
    }

	public void SetNetTile()
	{
		MatNormal = MatNet;
		GetComponent<Renderer>().material = MatNormal;
		bIsNetTile = true;
	}

    public void Select()
    {
        GetComponent<Renderer>().material = MatSelected;

		string team = GameManager.Instance.CurrentTurn;
		Player p = null;

		for(int i = 0; i < Players.Count; i++)
		{
			if(!Players[i].bIsKeeper && Players[i].Team == team)
			{
				p = Players[i];
				break;
			}
		}

		GameManager.Instance.SetSelectedPlayer(p);
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = MatNormal;
    }

	public void AddPlayer(Player p)
	{
		Players.Add(p);
	}

	public void RemovePlayer(Player p)
	{
		Players.Remove(p);
	}

	public bool HasPlayerOnTeam(string team)
	{
		for(int i = 0; i < Players.Count; i++)
		{
			if(Players[i].Team == team)
				return true;
		}

		return false;
	}

	public Player GetPlayer(string team)
	{
		for(int i = 0; i < Players.Count; i++)
		{
			if(Players[i].Team == team)
				return Players[i];
		}

		return null;
	}
}
