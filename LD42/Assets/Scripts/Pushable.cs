using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    bool canMove = false;
    Vector3 targetPos;
    public float speed;

    public GameObject islandTiles;
    Tiles tiles;

    bool onGround = true;

    // Use this for initialization
    void Start () {
        tiles = islandTiles.GetComponent<Tiles>();
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

    //////////////////////////////////////////////////////
    //Temporary solution!!!!!!!!!!!!!!!!!!!!!
    //TODO: VERY INNEFICIENT, But tile checking buggs out
    //Check for exiting a collider called tile and sets onground to false if we do
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            onGround = false;
            StartCoroutine("CheckDelayed");
        }
    }
    //If we are colliding again set onground to true
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            onGround = true;
            StartCoroutine("CheckDelayed");
        }
    }
    //Checks if we are on the ground and calls die if we are not
    void Check()
    {
        if(onGround == false)
        {
            Die();
        }
    }
    //Delays the check so if we are moving to a new tile it has time to enter that collider and set onground to true again,
    //if we did not do this the object would be destroyed before we colide again
    IEnumerator CheckDelayed()
    {
        yield return new WaitForSeconds(0.6f);
        Check();
    }
    //End of code that has to be remade
    ///////////////////////////////////
    
}
