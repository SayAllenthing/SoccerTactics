using UnityEngine;
using System.Collections;

public class FieldGenerator : MonoBehaviour {

    public GameObject Tile;

    public int Width = 10;
    public int Length = 18;

    float size = 1.01f;

	// Use this for initialization
	void Start () 
    {
        CreateField();
	}

    void CreateField()
    {
        GameObject g = GameObject.Instantiate(new GameObject()) as GameObject;
        g.name = "Field";


        for (int x = -Width / 2; x < Width / 2; x++)
        {
            for (int y = -Length / 2; y < Length / 2; y++)
            {
                Vector3 pos = new Vector3(y * size, 0, x * size);
                GameObject tile = GameObject.Instantiate(Tile, pos, Quaternion.identity) as GameObject;

                tile.transform.parent = g.transform;
            }
        }
    }
}
