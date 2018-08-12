using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Tiles").GetComponent<Tiles>().SetStartPos(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
