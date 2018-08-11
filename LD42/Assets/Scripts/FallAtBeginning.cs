using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAtBeginning : MonoBehaviour {

    [Tooltip("Maximum time that this object will fall after")]
    public float maxTime;

	// Use this for initialization
	void Start () {
        //Calls die after a random amount of time
        float random = Random.Range(0.5f, maxTime);
        Invoke("Die", random);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Die()
    {
        Destroy(gameObject);
    }
}
