using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    //Get acces to the tiles class
    private Tiles tiles;
    //Movement
    private Vector3 posToMove;
    private bool canMove = false;
    [Tooltip("Speed at which you move to a new position")]
    public float speed;


    private bool areColliding = false;
	// Use this for initialization
	void Start () {
        tiles = GameObject.FindGameObjectWithTag("Tiles").GetComponent<Tiles>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCharacter>().FollowMe(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //We don't want input when we are colliding with a object that we are pushing
        if (!areColliding)
        {
            //Simple Input
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Left();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Right();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Up();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Down();
            }
        }
        //Moves to the postomove smoothly when canmove is set to true through input
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMove, speed * Time.deltaTime);
            if(transform.position == posToMove)
            {
                canMove = false;
            }
        }
    }

    //Check if we are facing right, if not face right.
    //If yes, calculate target position and do the checks on that
    void Right()
    {
        if(transform.rotation.eulerAngles.z != 270)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            Vector3 targetPos = new Vector3(transform.position.x + 1, transform.position.y);
            DoChecks(targetPos);
        }
    }
    void Left()
    {
        if (transform.rotation.eulerAngles.z != 90)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            Vector3 targetPos = new Vector3(transform.position.x - 1, transform.position.y);
            DoChecks(targetPos);
        }
    }
    void Up()
    {
        if (transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 1);
            DoChecks(targetPos);

        }
    }
    void Down()
    {
        if (transform.rotation.eulerAngles.z != 180)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
           Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - 1);
           DoChecks(targetPos);
        }
    }

    //Get the position in the direction of the player behind the target, check if there is a tile there
    //If so, push and return true to notify succesfull, else return false and don't push
    public bool Push(GameObject target)
    {
        Vector3 pushablePos = target.transform.position + transform.up;
        
        //If there is anything
        if (tiles.WhatIsAt(pushablePos) != null)
        {
            //Loop through all the colliders looking for tiles, and immovables
            GameObject tile = null;
            GameObject immovable = null;
            RaycastHit2D[] thingsAtPos = tiles.WhatIsAt(pushablePos);
            foreach (RaycastHit2D thing in thingsAtPos)
            {
                if (thing.collider.tag == "Tile")
                {
                    tile = thing.collider.gameObject;
                }
                else if (thing.collider.tag == "Immovable")
                {
                    immovable = thing.collider.gameObject;
                }
            }

            //If there is a tile and not an immovable object
            if (tile != null && immovable == null)
            {
                target.GetComponent<Pushable>().MoveTo(pushablePos);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void DoChecks(Vector3 targetPos)
    {
        //If canMove is false, we can set a new position and move
        //This is true when we are already moving
        if (!canMove)
        {
            //If we have colliders at the target position
            if (tiles.WhatIsAt(targetPos) != null)
            {
                //Get all the colliders at this position and check if there are more then one
                RaycastHit2D[] thingsAtPos = tiles.WhatIsAt(targetPos);
                if (thingsAtPos.Length > 1)
                {
                    //Loop through the colliders to find if we have a gameobject with the tag pushable
                    GameObject target = null;
                    GameObject immovable = null;
                    foreach (RaycastHit2D thing in thingsAtPos)
                    {
                        if (thing.collider.tag == "Pushable") 
                        {
                            target = thing.collider.gameObject;
                        }
                        else if (thing.collider.tag == "Immovable")
                        {
                            immovable = thing.collider.gameObject;
                        }
                    }
                    if(immovable != null)
                    {
                        //There is an immovable object in that position
                    }
                    //If there is no immovable object
                    else
                    {
                        //If there is a pushable object
                        if (target != null)
                        {
                            //If it's true, the object is pushed, so we can move to that position.
                            //If it's false, the object couldn't be pushed so we don't move there.
                            if (Push(target) == true)
                            {
                                posToMove = targetPos;
                                canMove = true;
                            }
                        }
                    }
                }
                //We can move regardless if there is only one collider because that will be a tile
                else
                {
                    posToMove = targetPos;
                    canMove = true;
                }
            }
        }
    }

    //Check if we are in the middle of pushing an object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Pushable")
        {
            areColliding = true;
        }
    }
    //Check if we just stopped pushing an object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Pushable")
        {
            areColliding = false;
        }
    }
}
