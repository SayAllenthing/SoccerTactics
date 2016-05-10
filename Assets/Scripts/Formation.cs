using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation : MonoBehaviour 
{
    public List<Vector2> Positions = new List<Vector2>();

    public static List<Vector2> FourFourTwo()
    {
        List<Vector2> Pos = new List<Vector2>();

        //Fowards
        Pos.Add(new Vector2(0, -1));
        Pos.Add(new Vector2(0, 1));

        //Midefield
        Pos.Add(new Vector2(-5, -4));
        Pos.Add(new Vector2(-5, -1));
        Pos.Add(new Vector2(-5, 1));
        Pos.Add(new Vector2(-5, 4));

        //Defenders
        Pos.Add(new Vector2(-9, -4));
        Pos.Add(new Vector2(-10, -1));
        Pos.Add(new Vector2(-10, 1));
        Pos.Add(new Vector2(-9, 4));

        return Pos;
    }

}
