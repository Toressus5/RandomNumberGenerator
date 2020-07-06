using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    public Transform StartPosition;
    public Transform TargetPosition; 
	List<Node> OpenList = new List<Node>();
 	List<Node> NeighborNodeList = new List<Node>();   
	HashSet<Node> ClosedList = new HashSet<Node>();

    Node CurrentNode;

    
    // Pseudo code
    // Calculate the values of each neighbor of the current node (And add the neighbors to the open list)
    // Choose the node (Out of the openlist) with the lowest Fcost and make this the currentNode
    // Make the previous node the parent of the current node so it can track where it came from
    // Repeat till:
    // if the Fcost is equal then go for the lowest HCost and return to the previous loop
    // if all the costs are equal, choose one at random and return to the previous loop
    //
    void Start() {
        grid = GetComponent<Grid>();        
    }

    void Update()
    {
        FindThePath(StartPosition.position, TargetPosition.position);
    }
    
    void FindThePath(Vector3 _StartPosition, Vector3 _TargetPosition){
        Node StartNode;
        Node TargetNode;

        //OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];

            /*if (CurrentNode == TargetNode)
            {
                ReversePath();
            }*/

            foreach (Node NeighborNode in NeighborNodeList)
            {
                if (!NeighborNode.walkable)
                {
                    continue;
                }

                /*if (CurrentNode.gCost > NeighborNode.gCost)
                {
                    CurrentNode = NeighborNode;
                }*/
            }

        }

    }
    void ReversePath(){

    }    


}
