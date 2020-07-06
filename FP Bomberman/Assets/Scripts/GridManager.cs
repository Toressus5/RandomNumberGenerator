using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    // Physical Grid
    // I think i need to clean it up. Looking forward to some feedback
    [SerializeField]
    private GameObject Indestructible;
    [SerializeField]
    private GameObject Destructible;
    [SerializeField]
    private GameObject Tile;    
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;    
    [Range(0,100)]
    public int desRockSpawnRate;

    public GameObject Grid;

    //Pathfinding grid
    Vector2 WorldSize;
	Node[,] grid;
    public LayerMask unwalkableMask;

	int gridSizeX, gridSizeY;
    public float nodeRadius = 1;          
    int nodeDiameter;

    private List<GameObject> destructibleWalls = new List<GameObject>();

    void Start()
    {
        WorldSize = new Vector2(height, width);
	    nodeDiameter = Mathf.RoundToInt(nodeRadius*2);        
		gridSizeX = Mathf.RoundToInt(WorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(WorldSize.y/nodeDiameter);
        CreateGrid(width, height);
		CreatePathfindingGrid();
    }
    //Physical grid
    private void CreateGrid(int width, int height)
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                var tile = Instantiate(Tile, new Vector3(x * nodeDiameter - WorldSize.x/2  + nodeRadius, -1f, z * nodeDiameter - WorldSize.y/2 + nodeRadius), Quaternion.identity);
                tile.transform.SetParent(Grid.transform, true);

                if (x % 2 == 0 && z % 2 == 0) //When it's even number it will generate an indestructible rock/stone/etc. (We need to decide on a name)
                {
                    var indestructible = Instantiate(Indestructible, new Vector3(x * nodeDiameter - WorldSize.x/2 + nodeRadius, 0, z * nodeDiameter - WorldSize.y/2 + nodeRadius), Quaternion.identity);
                    indestructible.transform.SetParent(Grid.transform, true);
                }
                else if((x % 2 == 1 && z % 2 == 1) && UnityEngine.Random.Range(0,100) < desRockSpawnRate) //When it's uneven number it will generate a destructible rock based on a percentage and a random number
                {
                    GameObject destructible = Instantiate(Destructible, new Vector3(x * nodeDiameter - WorldSize.x/2  + nodeRadius, 0, z * nodeDiameter - WorldSize.y/2 + nodeRadius), Quaternion.identity);
                    destructible.transform.SetParent(Grid.transform, true);
                    destructibleWalls.Add(destructible);
                }
            }
        }
        Instantiate(Player, new Vector3((width/2) -1, 0, (height/2) -1), Quaternion.identity);
        Debug.Log(destructibleWalls.Count);
        AttachKeyDrop();
        AttachTrapDoorDrop();
    }

    private void AttachKeyDrop()
    {
        int randNum = UnityEngine.Random.Range(0, destructibleWalls.Count);
        destructibleWalls[randNum].GetComponent<DestructableLootDrop>().keyDrop = true;
        Debug.Log(destructibleWalls[randNum].transform.position);
    }
    private void AttachTrapDoorDrop()
    {
        int randNum = UnityEngine.Random.Range(0, destructibleWalls.Count);
        destructibleWalls[randNum].GetComponent<DestructableLootDrop>().trapDoorDrop = true;
        Debug.Log(destructibleWalls[randNum].transform.position);

    }

    //Pathfinding Grid
    void CreatePathfindingGrid() {
		grid = new Node[gridSizeX,gridSizeY];
        Vector3 bottomLeftCorner = transform.position - Vector3.right * WorldSize.x/2 - Vector3.forward * WorldSize.y/2; // Finding the bottom left of the grid
		for (int x = 0; x < gridSizeX; x ++) { //Looping through each node and giving them a position, checking if they are walkable and then putting them in the grid.
			for (int y = 0; y < gridSizeY; y ++) {
                Vector3 NodePosition = bottomLeftCorner + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(NodePosition,nodeRadius - 0.1f ,unwalkableMask));
                grid[x,y] = new Node(walkable, NodePosition);
			}
		}
	}


    void ConvertWorldPositionToNode(){
        // Something to convert world positions of for example the start and target position into one of the nodes
    }

    public List<Node> GetNeighboreNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();

        // Still need to get the neighbors

        return NeighborList;
    }
    
    // Gizmos for the grid
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(WorldSize.x,1,WorldSize.y));
		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.NodePosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}

}
