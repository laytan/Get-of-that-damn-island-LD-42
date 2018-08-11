using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {


    Vector3 posToMove;
    bool canMove = false;
    public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
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

        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMove, speed * Time.deltaTime);
            if(transform.position == posToMove)
            {
                canMove = false;
            }
        }
    }

    void Right()
    {
        if(transform.rotation.eulerAngles.z != 270)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            if (!canMove)
            {
                posToMove = new Vector3(transform.position.x + 1, transform.position.y);
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
            if (!canMove)
            {
                posToMove = new Vector3(transform.position.x - 1, transform.position.y);
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
            if (!canMove)
            {
                posToMove = new Vector3(transform.position.x, transform.position.y + 1);
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
            if (!canMove)
            {
                posToMove = new Vector3(transform.position.x, transform.position.y - 1);
                canMove = true;
            }
        }
    }
}
