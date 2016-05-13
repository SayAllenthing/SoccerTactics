using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public static Ball TheBall;

	public int X;
	public int Y;

	Player Owner;

	public AnimationCurve HeightCurve;

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

	public void Move(int x, int y, bool ForcePosition = true)
	{
		X = x;
		Y = y;

		if(ForcePosition)
		{
			Tile t = Field.Map[x][y];
			transform.position = t.transform.position + new Vector3(0.25f, 0.25f, -0.15f);
		}
	}

	public void Pass(Player p)
	{
		p.GetBall();
		Kick(p.MyTile);
	}

	public void PassFail(Tile t)
	{
		Owner.LoseBall();
		Kick(t);

		if(t.Players.Count > 0)
		{
			t.Players[0].GetBall();
		}
	}

	public void Shoot(Tile t)
	{
		Owner.LoseBall();
		Kick(t);
	}

	void Kick(Tile t)
	{
		float time = GetTravelTime(t.X, t.Y);

		StartCoroutine(MoveObject(t.transform.position + new Vector3(0.25f, 0.25f, -0.15f), time));
		Move(t.X, t.Y, false);
	}

	float GetTravelTime(int x, int y)
	{
		int dx = Mathf.Abs(x - X);
		int dy = Mathf.Abs(y - Y);

		return (float)(dx + dy) * 0.1f;
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
			Vector3 pos = Vector3.Lerp(startPos, endPos, i);
			pos.y = 0.25f + HeightCurve.Evaluate(i) * 2;
			transform.position = pos;
			yield return new WaitForEndOfFrame();
		}

		yield return null;
	}
}
