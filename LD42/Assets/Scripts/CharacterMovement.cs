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

    public Animator anim;

    private Vector3 orientation;

    public AudioClip push, moving;
    private SoundManager sm;
    private bool areColliding = false;
	// Use this for initialization
	void Start () {
        tiles = GameObject.FindGameObjectWithTag("Tiles").GetComponent<Tiles>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCharacter>().FollowMe(this.gameObject);
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //We don't want input when we are colliding with a object that we are pushing
        if (!areColliding)
        {
            //Simple Input
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (orientation != -transform.right)
                {
                    orientation = -transform.right;
                    anim.SetInteger("direction", 3);
                }
                else
                {
                    Left();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (orientation != transform.right)
                {
                    orientation = transform.right;
                    anim.SetInteger("direction", 1);
                }
                else
                {
                    Right();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(orientation != transform.up)
                {
                    orientation = transform.up;
                    anim.SetInteger("direction", 2);
                }
                else
                {
                    Up();
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(orientation != -transform.up)
                {
                    orientation = -transform.up;
                    anim.SetInteger("direction", 0);
                }
                else
                {
                    Down();
                }
            }
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
               Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
               Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || 
               Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
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
        Vector3 targetPos = new Vector3(transform.position.x + 1, transform.position.y);
        DoChecks(targetPos);
    }
    void Left()
    {
        Vector3 targetPos = new Vector3(transform.position.x - 1, transform.position.y);
        DoChecks(targetPos);
    }
    void Up()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y + 1);
        DoChecks(targetPos);
    }
    void Down()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - 1);
        DoChecks(targetPos);
    }

    //Get the position in the direction of the player behind the target, check if there is a tile there
    //If so, push and return true to notify succesfull, else return false and don't push
    public bool Push(GameObject target)
    {   
        Vector3 pushablePos = target.transform.position + orientation;
        
        //If there is anything
        if (tiles.WhatIsAt(pushablePos) != null)
        {
            //Loop through all the colliders looking for certain objects
            GameObject tile = null;
            GameObject immovable = null;
            GameObject pushable = null;
            GameObject goal = null;
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
                else if(thing.collider.tag == "Pushable")
                {
                    pushable = thing.collider.gameObject;   
                }
                else if(thing.collider.tag == "Goal")
                {
                    goal = thing.collider.gameObject;
                }
            }

            //If there is a tile and not an immovable object or pushable object or goal
            if (tile != null && immovable == null && pushable == null && goal == null)
            {
                target.GetComponent<Pushable>().MoveTo(pushablePos);
                return true;
            }
            else if(goal != null)
            {
                //trying to push into goal
                if(target.gameObject.name == "target")
                {
                    target.GetComponent<Pushable>().MoveTo(pushablePos);
                    EndSequence(target.gameObject, goal);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void EndSequence(GameObject goal, GameObject ship)
    {
        GameObject.Find("target").GetComponent<Ship>().StartCoroutine("PlayEndSequence");
        Invoke("ui.ToWinScreen",5);
        Destroy(gameObject, 1.5f);

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
                                sm.PlaySound(moving);
                            }
                        }
                    }
                }
                //We can move regardless if there is only one collider because that will be a tile
                else
                {
                    posToMove = targetPos;
                    canMove = true;
                    sm.PlaySound(moving);
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
            sm.PlaySound(push);
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
