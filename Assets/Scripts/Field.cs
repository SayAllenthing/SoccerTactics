﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : MonoBehaviour 
{
    public static Field Instance;
    public static List<List<Tile>> Map = new List<List<Tile>>();
	public List<Tile> NetTiles = new List<Tile>();

    public GameObject Tile;
    public GameObject HomePlayer;
    public GameObject AwayPlayer;
	public GameObject BallPrefab;

    public int Width = 15;
    public int Length = 28;

    float size = 1.05f;

	GameObject TheField;

    // Use this for initialization
    void Start () 
    {
        if(Instance != null)
            Instance = null;

        Instance = this;

        GenerateField();
    }

    void GenerateField()
    {
		TheField = new GameObject();
		TheField.name = "Field";

        for (int x = 0; x < Length; x++)
        {
            List<Tile> row = new List<Tile>();
            for (int y = 0; y < Width; y++)
            {                
				Tile tile = CreateTile(x,y);
                row.Add(tile);
            }

            Map.Add(row);
        }

		CreateNetTiles();
		CreateBall(8, Width/2);

        CreateTeam(true);
        CreateTeam(false);


    }

	void CreateNetTiles()
	{
		//Home Net
		CreateNetTile(-1, Width/2);
		CreateNetTile(-1, Width/2+1);
		CreateNetTile(-1, Width/2-1);

		//Away
		CreateNetTile(Length, Width/2);
		CreateNetTile(Length, Width/2+1);
		CreateNetTile(Length, Width/2-1);
	}

	Tile CreateTile(int x, int y)
	{
		Vector3 pos = new Vector3( (x -Length/2) * size, 0, (y -Width/2) * size);
		GameObject go = GameObject.Instantiate(Tile, pos, Quaternion.identity) as GameObject;

		Tile tile = go.GetComponent<Tile>();

		tile.transform.parent = TheField.transform;
		tile.SetPosition(x, y);

		return tile;
	}

	void CreateNetTile(int x, int y)
	{
		Vector3 pos = new Vector3( (x -Length/2) * size, 0, (y -Width/2) * size);
		GameObject go = GameObject.Instantiate(Tile, pos, Quaternion.identity) as GameObject;

		Tile tile = go.GetComponent<Tile>();

		tile.transform.parent = TheField.transform;
		tile.SetPosition(x, y);

		tile.SetNetTile();
		NetTiles.Add(tile);
	}

    void CreateTeam(bool bHome)
    {
        List<Vector2> Positions = Formation.FourFourTwo();

        int Forward = 1;
        int StartY = Width / 2;
        int StartX = Length / 2 - 1;

        GameObject Prefab = HomePlayer;

        if (!bHome)
        {
            StartX += 1;
            Forward = -1;
            Prefab = AwayPlayer;
        }
        int ModX = 0 * Forward;

        for (int i = 0; i < Positions.Count; i++)
        {
            int x = (int)(Positions[i].x * Forward) + StartX + ModX;
            int y = (int)Positions[i].y + StartY;

			CreatePlayer(new Vector2(x, y), Prefab, bHome, 11-i);
        }

        //Keeper
        int KeeperX = StartX - ((Length/2 - 1) * Forward);
        CreatePlayer(new Vector2(KeeperX , StartY), Prefab, bHome, 1, true);
    }

	void CreatePlayer(Vector2 pos, GameObject Player, bool home, int number, bool bIsKeeper = false)
    {
        Tile t = Field.Map[(int)pos.x][(int)pos.y];
		GameObject p = GameObject.Instantiate(Player, t.transform.position, Quaternion.identity) as GameObject;

		string team = home ? "Home" : "Away";

		p.GetComponent<Player>().Init((int)pos.x, (int)pos.y, bIsKeeper, team, number);
    }

	void CreateBall(int x, int y)
	{
		Tile t = Field.Map[x][y];
		GameObject p = GameObject.Instantiate(BallPrefab, t.transform.position + (Vector3.up * 0.5f), Quaternion.identity) as GameObject;
		Ball.TheBall.Init(x,y);
	}

    public void SelectTile(Tile t)
    {
		if(t.bIsNetTile)
			return;
		
        DeselectTiles();
        t.Select();
    }

    public void DeselectTiles()
    {
        for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++)
                Map[x][y].Deselect();
    }

    public Tile GetTile(int x, int y)
    {
        x = Mathf.Clamp(x, 0, Length - 1);
        y = Mathf.Clamp(y, 0, Width - 1);

        return Map[x][y];
    }
}
