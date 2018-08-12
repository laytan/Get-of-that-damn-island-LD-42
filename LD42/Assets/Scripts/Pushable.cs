using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    bool canMove = false;
    Vector3 targetPos;

    [Tooltip("The speed that the object gets pushed at")]
    public float speed;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        //If we want the entity to move to a new position, move there, when there set canMove to false
		if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

            if(transform.position == targetPos)
            {
                canMove = false;
            }
        }
	}

    //Used by player when trying to push, just sets the pos it needs to go and lets the Update know it can go
    public void MoveTo(Vector3 pos)
    {
        if(canMove == false)
        {
            targetPos = pos;
            canMove = true;
        }
    }

    //TODO: Temporary just destroy the gameobject
    void Die()
    {
        Destroy(gameObject);
    }
}
