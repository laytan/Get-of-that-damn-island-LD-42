using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    bool canMove = false;
    Vector3 targetPos;
    public float speed;

    public GameObject islandTiles;
    Tiles tiles;

    // Use this for initialization
    void Start () {
        tiles = islandTiles.GetComponent<Tiles>();
    }
	
	// Update is called once per frame
	void Update () {
		if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

            if(transform.position == targetPos)
            {
                canMove = false;
            }
        }
	}

    public void MoveTo(Vector3 pos)
    {
        if(canMove == false)
        {
            targetPos = pos;
            canMove = true;
        }

        Debug.Log(pos);
        if (tiles.IsThereATileAt(targetPos))
        {
            Debug.Log("Valid");
        }
        else
        {
            Debug.Log("invalid");
        }
    }
}
