using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingBehaviour : MonoBehaviour
{
    public int gridWidth;
    public int gridHeight;
    public Tile[] tiles;
    public GameObject prefab;
    // Start is called before the first frame update
    
    private void Start()
    {
        tiles = new Tile[gridWidth * gridHeight];
        Vector3 offset = Vector3.zero;
        //spawn all the cubes
        for(int i = 0; i < gridHeight; i++)
        {
            for(int j = 0; j < gridWidth; j++) 
            {
                GameObject newTile = Instantiate(prefab, transform.position + offset, transform.rotation);
                tiles[i * gridWidth + j] = newTile.GetComponent<Tile>();
                offset.x += 1.0f;
            }
            offset.x = 0f;
            offset.z += 1.0f;
        }
    }
    private int GetTileIndex(Tile target)
    {
        for(int i = 0; i < tiles.Length; i++) 
        {
            if (tiles[i] == target) 
            {
                return i;
            }
        }
        return -1;//-1 means not found
    }
    private Tile GetCheapestTile(Tile[] arr)
    {
        float bestGSCore = float.MaxValue;
        Tile bestTile = null;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (arr[i].gScore < bestGSCore)
            {
                bestTile = arr[i];
                bestGSCore = arr[i].gScore;
            }
        }
        return bestTile;
    }
    
    public Tile[] calculatePath(Tile origin, Tile destination) 
    {
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(origin);
        while (openList.Count != 0 && !closedList.Contains(destination))
        {
            Tile current = GetCheapestTile(openList.ToArray());
            openList.Remove(current);

            closedList.Add(current);
            int currentIdx = GetTileIndex(current);
            //hardcoding cost of 1 since they spawn 1 unit away from each other
            //left
        }
        return tiles;
    }
    
 
}
