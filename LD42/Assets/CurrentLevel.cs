using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevel : MonoBehaviour {

    public int lvl;
	// Use this for initialization
	void Start () {
		
	}

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
