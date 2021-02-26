using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile previousTile;
    public Tile[] connectedTiles;
    public int gScore;
    public Tile right;
    public Tile left;
    public Tile top;
    public Tile bottom;

    public int CompareTo(Tile other) 
    {
        return -1;
    }
}

