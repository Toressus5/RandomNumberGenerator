using System.Collections;
using UnityEngine;

public class Node {
	
	public bool walkable;
	public Vector3 NodePosition;
	
	public Node(bool _walkable, Vector3 _NodePosition) {
		walkable = _walkable;
		NodePosition = _NodePosition;
	}
}
