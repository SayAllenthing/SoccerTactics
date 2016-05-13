using UnityEngine;
using System.Collections;

public class GameActionResolver : MonoBehaviour 
{
	public static GameActionResolver Instance;

    public Tile FailedTile;

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

        Debug.Log("Pass Difficulty: " + difficulty + ", Roll: " + roll + "+(" + (p1.GetStats().coreStats.Passing / 2) + ")");

        if (final >= difficulty)
            return true;
        else
        {
            int x = t2.X;
            int y = t2.Y;
            while (x == t2.X && y == t2.Y)
            {
                x = t2.X + Random.Range(-2, 2);
                y = t2.Y + Random.Range(-2, 2);
            }

            FailedTile = Field.Instance.GetTile(x, y);
        }

		return false;
	}
}
