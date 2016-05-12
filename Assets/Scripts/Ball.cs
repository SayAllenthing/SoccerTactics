using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public static Ball TheBall;

	public int X;
	public int Y;

	Player Owner;

	// Use this for initialization
	void Awake () 
	{
		if(TheBall != null)
		{
			TheBall = null;
		}

		TheBall = this;
	}

	public void Init(int x, int y)
	{
		Move(x,y);
	}

	public void Move(int x, int y)
	{
		X = x;
		Y = y;

		Tile t = Field.Map[x][y];
		transform.position = t.transform.position + new Vector3(0.25f, 0.25f, -0.15f);
	}

	public void Pass(Player p)
	{
		p.GetBall();
		Kick(p.X, p.Y);
	}

	public void Shoot(Tile t)
	{
		Owner.LoseBall();
		StartCoroutine(MoveObject(t.transform.position + new Vector3(0.25f, 0.25f, -0.15f), 1f));
	}

	void Kick(int destX, int destY)
	{
		Tile t = Field.Map[destX][destY];

		StartCoroutine(MoveObject(t.transform.position + new Vector3(0.25f, 0.25f, -0.15f), 1f));
	}

	public void SetOwner(Player p)
	{
		if(Owner)
			Owner.LoseBall();

		Owner = p;
	}

	//Co-routines
	IEnumerator MoveObject(Vector3 endPos, float time)
	{
		float i= 0.0f;
		float rate= 1.0f/time;
		Vector3 startPos = transform.position;
		while (i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			transform.position = Vector3.Lerp(startPos, endPos, i);
			yield return new WaitForEndOfFrame();
		}

		yield return null;
	}
}
