using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int X;
	public int Y;

	public Tile MyTile;

	public bool bIsKeeper = false;
	public string Team = "Home";

	bool bHasBall = false;

	int MoveDist = 2;

	int InitialX;
	int InitialY;

	PlayerStats Stats;

	public void Init(int x, int y, bool keeper, string team, int number)
	{		
		Move(x,y);
		if(keeper)
			SetKeeper();

		InitialX = x;
		InitialY = y;

		Team = team;

		Stats = new PlayerStats();
		Stats.GeneratePlayer();
		Stats.Name = team + " Player " + number;
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

		if(bHasBall)
			Ball.TheBall.Move(x,y);
		else
			CheckForBall();
			
	}

	public bool IsLegalMove(int x, int y)
	{
		int diffX = Mathf.Abs(X - x);
		int diffY = Mathf.Abs(Y - y);

		if(diffX == 0 && diffY == 0)
			return false;

		return diffX <= MoveDist && diffY <= MoveDist;
	}

	void CheckForBall()
	{
		Ball b = Ball.TheBall;

		int x = b.X;
		int y = b.Y;

		if(X == x && Y == y)
			GetBall();
	}

	public void GetBall()
	{
		bHasBall = true;
		Ball.TheBall.SetOwner(this);
	}

	public void LoseBall()
	{
		bHasBall = false;
	}

	public bool HasBall() { return bHasBall; }

	public PlayerStats GetStats() { return Stats; }
}
