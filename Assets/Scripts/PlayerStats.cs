using UnityEngine;
using System.Collections;

public class PlayerStats
{
	public string Name;

	public struct CoreStats
	{
		public int Passing;
		public int Shooting;
		public int Tackling;
	};

	public CoreStats coreStats;

	public void GeneratePlayer()
	{
		coreStats.Passing = Random.Range(5,15);
		coreStats.Shooting = Random.Range(5,15);
		coreStats.Tackling = Random.Range(5,15);
	}
}
