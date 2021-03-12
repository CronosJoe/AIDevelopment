using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Data")]
    public int gScore; //Actual cost
    public int fScore; //combined cost to prioritize where to look  
    public int hScore;// direct cost
    public Tile previousTile;//used for tracking which tile came before this one got set to the current


    public void CalculateFScore() 
    {
        fScore = gScore + hScore;
    }

}
