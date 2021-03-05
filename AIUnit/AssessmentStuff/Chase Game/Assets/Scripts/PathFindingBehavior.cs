using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingBehavior : MonoBehaviour
{
    [Header("Changeables")]
    public int GridWidth;
    public int GridHeight;
    public Tile[,] tiles;
    public GameObject prefab;

    private int foundHeight;
    private int foundWidth;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[GridWidth, GridHeight];
        spawnTiles();
    }

    void spawnTiles() 
    {
        Vector3 offset = Vector3.zero;
        for (int i=0; i < GridWidth;i++) 
        {
            for(int j = 0; j < GridHeight;j++) 
            {
                GameObject newTile = Instantiate(prefab, transform.position + offset, transform.rotation);
                tiles[i,j] = newTile.GetComponent<Tile>();
                offset.x += 1.0f;
            }
            offset.x = 0f;
            offset.z += 1.0f;
        }
    }
    void findIndex(Tile target) //this will only really work once the tile has a GScore I think
    {
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                if(tiles[i,j] == target) 
                {
                    foundHeight = j;
                    foundWidth = i;
                    return;
                }
            }
        }
    }
    private Tile GetCheapestTile()
    {
        float bestGSCore = float.MaxValue;
        Tile bestTile = null;

        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                if (tiles[i,j].gScore < bestGSCore)
                {
                    bestTile = tiles[i, j];
                    bestGSCore = tiles[i, j].gScore;
                }
            }
        }
        return bestTile;
    }

    //TODO add in a distance/target method to find a tile
    void distanceToTarget(Tile origin, Tile destination) 
    {
     //redo this entire method last implementation only worked in 2 directions

    }
}
