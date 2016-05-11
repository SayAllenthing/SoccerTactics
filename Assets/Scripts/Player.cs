using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int X;
	public int Y;

	public Tile MyTile;

	public bool bIsKeeper = false;
	public string Team = "Home";

	public void Init(int x, int y, bool keeper, string team)
	{		
		Move(x,y);
		if(keeper)
			SetKeeper();

		Team = team;
	}

	void SetKeeper()
	{
		bIsKeeper = true;
	}

	public void Move(int x, int y)
	{
		if(MyTile != null)
		{
			MyTile.RemovePlayer(this);
			MyTile = null;
		}

		MyTile = Field.Map[x][y];
		X = x;
		Y = y;

		transform.position = MyTile.transform.position;

		MyTile.AddPlayer(this);
	}
}
