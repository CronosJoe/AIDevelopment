using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingBehavior : MonoBehaviour
{
    //note this script has been gutted to simply be a map spawner
    [Header("Changeables")]
    public int GridWidth;
    public int GridHeight;
    public Tile[,] tiles;
    public GameObject prefab;
    [Header("Constants")]
    private const int DiagonalCost = 14;
    private const int StraightCost = 10;


    private List<Tile> openList;
    private List<Tile> closedList;

    public int foundHeight;
    public int foundWidth;
    // Start is called before the first frame update
    private void Start()
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
                tiles[i, j].isWalkable = true; //starting off each tile as walkable
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
    private Tile GetCheapestFTile(List<Tile> openList)
    {
        Tile bestTile = null;
        bestTile.fScore = int.MaxValue; //this will be overwritten with the other tiles
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].fScore < bestTile.fScore)
            {
                bestTile = openList[i];//overwriting
            }
        }
        return bestTile; //returning the lowest fScore node from the open list
    }
    private List<Tile> FindPath(Tile startNode, Tile targetNode) //we're going to assume I have a node that the ai will start on, probably through raycast or collision, and then we will return the pathing
    {
        openList = new List<Tile> { startNode }; //starting with the startnode in the open list
        closedList = new List<Tile>();

        for (int i = 0; i < GridWidth; i++)//setting up the G cost in our grid just gonna start them all off by setting to max value
        {
            for (int j = 0; j < GridHeight; j++)
            {
                tiles[i, j].gScore = int.MaxValue;
                tiles[i, j].CalculateFScore();
                tiles[i, j].previousTile = null; //just in case I previously calculated a path.
            }
        }

        startNode.gScore = 0; //starting here takes no cost to not move
        startNode.hScore = CalculateDistanceCost(startNode, targetNode);
        startNode.CalculateFScore();

        while (openList.Count > 0)
        {
            Tile currentNode = GetCheapestFTile(openList);
            if(currentNode == targetNode)
            {
                return CalculatePath(targetNode);
            }

            openList.Remove(currentNode);//removing the last node we checked
            closedList.Add(currentNode); //adding it to the checked list

            foreach(Tile neighborTile in GetNeighbors(currentNode))//should iterate through every neighbor
            {
                if (closedList.Contains(neighborTile)) continue;
                if (!neighborTile.isWalkable) 
                {
                    closedList.Add(neighborTile);
                    continue; //move onto next iteration!
                }
                int tentativeGCost = currentNode.gScore + CalculateDistanceCost(currentNode, neighborTile);
                if (tentativeGCost < neighborTile.gScore)
                {
                    neighborTile.previousTile = currentNode; //setting our path
                    neighborTile.gScore = tentativeGCost; //replacing GCost, and setting the other node scores
                    neighborTile.hScore = CalculateDistanceCost(neighborTile, targetNode); 
                    neighborTile.CalculateFScore();

                    if (!openList.Contains(neighborTile))
                    {
                        openList.Add(neighborTile);
                    }
                }
            }//foreach
        }//while
        //out of nodes meaning there is no possible path
    return null;
    }

    private List<Tile> GetNeighbors(Tile currentNode)
    {
        findIndex(currentNode); //gets me the index of the current node in foundHeight and foundWidth
        List<Tile> neighbors = new List<Tile>();
        //add the the top neighboring tiles
        if (!(foundHeight == 0)) //if the height is zero then the tiles above the current node simply wouldn't exist
        {
            neighbors.Add(tiles[foundHeight - 1,foundWidth-1]); //top left
            neighbors.Add(tiles[foundHeight - 1, foundWidth]); //directly above
            neighbors.Add(tiles[foundHeight - 1, foundWidth + 1]); //top right
        }
        //to the left
        if (!(foundWidth == 0)) //make sure there is a node to the left
        {
            neighbors.Add(tiles[foundHeight, foundWidth - 1]);
        }
        //to the right
        if(!(foundWidth == (GridWidth-1))) //make sure there is a node to the right
        {
            neighbors.Add(tiles[foundHeight, foundWidth + 1]);
        }
        //finally below
        if (!(foundHeight == (GridHeight-1)))
        {
            neighbors.Add(tiles[foundHeight + 1, foundWidth - 1]); //bottom left
            neighbors.Add(tiles[foundHeight + 1, foundWidth]); //directly below
            neighbors.Add(tiles[foundHeight + 1, foundWidth + 1]); //bottom right
        }
        return neighbors;
    }

    private List<Tile> CalculatePath(Tile targetNode)
    {
        List<Tile> path = new List<Tile>();
        path.Add(targetNode);
        Tile currentNode = targetNode; //setting up the start of our loop, and the start of the list, to use I will need to invert it
        while(currentNode.previousTile != null)//once we hit the null I set at the start we will be done with this loop!
        {
            path.Add(currentNode.previousTile);
            currentNode = currentNode.previousTile;//keep going till we find that end
        }
        path.Reverse();//flipp that pesky list for use!
        return path;
    }
    private int CalculateDistanceCost(Tile first, Tile second)
    {
        int xDistance = (int)Mathf.Abs(first.transform.position.x - second.transform.position.x); //even though it is 3d since we are on a platform without any thing to increase Y I am pretending it doesn't exist
        int zDistance = (int)Mathf.Abs(first.transform.position.z - second.transform.position.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return DiagonalCost * Mathf.Min(xDistance, zDistance) + StraightCost * remaining;
    }
}
