using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Data")]
    public int gScore; //used to calculate distance for ai
    public Tile previousTile;//used for tracking which tile came before this one got set to the current

}
