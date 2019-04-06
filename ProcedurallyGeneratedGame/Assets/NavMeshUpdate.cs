using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour {
    public NavMeshSurface navMesh;
	// Use this for initialization
	void Start () {
        navMesh.BuildNavMesh();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
