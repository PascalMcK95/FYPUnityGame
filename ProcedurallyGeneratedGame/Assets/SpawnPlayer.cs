using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
        Instantiate(player);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
