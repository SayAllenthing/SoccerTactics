using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : MonoBehaviour 
{
    public static List<List<Tile>> Map = new List<List<Tile>>();

    public GameObject Tile;
    public GameObject HomePlayer;
    public GameObject AwayPlayer;

    public int Width = 15;
    public int Length = 28;

    float size = 1.05f;

    // Use this for initialization
    void Start () 
    {
        GenerateField();
    }

    void GenerateField()
    {
        GameObject g = GameObject.Instantiate(new GameObject()) as GameObject;
        g.name = "Field";

        for (int x = 0; x < Length; x++)
        {
            List<Tile> row = new List<Tile>();
            for (int y = 0; y < Width; y++)
            {                
                Vector3 pos = new Vector3( (x -Length/2) * size, 0, (y -Width/2) * size);
                GameObject go = GameObject.Instantiate(Tile, pos, Quaternion.identity) as GameObject;

                Tile tile = go.GetComponent<Tile>();

                tile.transform.parent = g.transform;
                tile.SetPosition(x, y);
                row.Add(tile);
            }

            Map.Add(row);
        }

        CreateTeam(true);
        CreateTeam(false);
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

            CreatePlayer(new Vector2(x, y), Prefab);
        }

        //Keeper
        int KeeperX = StartX - ((Length/2 - 1) * Forward);
        CreatePlayer(new Vector2(KeeperX , StartY), Prefab);
    }

    void CreatePlayer(Vector2 pos, GameObject Player)
    {
        Tile t = Field.Map[(int)pos.x][(int)pos.y];
        GameObject.Instantiate(Player, t.transform.position, Quaternion.identity);
    }

    public void SelectTile(Tile t)
    {
        DeselectTiles();

        t.Select();
    }

    public void DeselectTiles()
    {
        for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++)
                Map[x][y].Deselect();
    }
}
