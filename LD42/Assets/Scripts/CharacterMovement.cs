using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public GameObject islandTiles;
    private Tiles tiles;

    Vector3 posToMove;
    bool canMove = false;
    public float speed;
    public bool areColliding = false;

	// Use this for initialization
	void Start () {
        tiles = islandTiles.GetComponent<Tiles>();
	}
	
	// Update is called once per frame
	void Update () {
        //Simple Input
        //If we are colliding disable movement (Pushables)
        if (!areColliding)
        {
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
    //If yes, set postomove a tile further and initiate movement with canmove
    //We check if canmove is false so that we don't override postomove mid moving.
    //We also check if the position where we want to go is a tile, so we don't fly/fall of the world
    void Right()
    {
        if(transform.rotation.eulerAngles.z != 270)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            Vector3 targetPos = new Vector3(transform.position.x + 1, transform.position.y);
            if (!canMove && tiles.IsThereATileAt(targetPos))
            {
                posToMove = targetPos;
                canMove = true;
            }
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
            if (!canMove && tiles.IsThereATileAt(targetPos))
            {
                posToMove = targetPos;
                canMove = true;
            }
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
            if (!canMove && tiles.IsThereATileAt(targetPos))
            {
                posToMove = targetPos;
                canMove = true;
            }

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
           if (!canMove && tiles.IsThereATileAt(targetPos))
           {
               posToMove = targetPos;
               canMove = true;
           }
        }
    }
}
