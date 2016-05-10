using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public Vector2 Position;

    public Material MatNormal;
    public Material MatSelected;

    public void SetPosition(int x, int y)
    {
        Position = new Vector2(x, y);
    }

    public void Select()
    {
        GetComponent<Renderer>().material = MatSelected;
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = MatNormal;
    }
}
