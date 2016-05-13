using UnityEngine;
using System.Collections;

public class GameActionResolver : MonoBehaviour 
{
	public static GameActionResolver Instance;

	void Start()
	{
		if(Instance != null)
			Instance = null;

		Instance = this;
	}

	public bool ResolvePass(Player p1, Player p2)
	{
		Tile t1 = p1.MyTile;
		Tile t2 = p2.MyTile;

		int difficulty = Mathf.Abs(t2.X - t1.X) +  Mathf.Abs(t2.Y - t1.Y);
		int roll = Random.Range(1,20);
		int final = roll + p1.GetStats().coreStats.Passing/2;

		if(final >= difficulty)
			return true;
		else
			Debug.Log("Failed Pass: " + final + " : " + difficulty + ", Roll " + roll);

		return false;
	}
}
