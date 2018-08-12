using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tiles : MonoBehaviour {

    private List<GameObject> tileOrderedOnDistance = new List<GameObject>();

    [Tooltip("The amount of seconds to wait before destroying a tile.")]
    public float tileDestroyDelay;

    private Vector3 startPos;
    private bool levelGenDone = false;

    // Use this for initialization
    public void LevelIsGenerated () {
        levelGenDone = true;  
    }
    public void SetStartPos(Vector3 pos)
    {
        if (levelGenDone)
        {
            startPos = pos;
            //Start of game initialize the ordered array on distance
            CreateTileIndex();
            //TODO: Uncomment to destroy island
            StartCoroutine("DestroyFurthestPossibleObject");
        }
    }

    //Populates tileOrderedOnDistance list
    //TODO: There is probably a better way of doing this! Only gets run once so is probably not a problem
    void CreateTileIndex()
    {
        //Get all the tiles, these are in a random order
        GameObject[] randomOrder = GameObject.FindGameObjectsWithTag("Tile");

        //Have 2 lists that puts the tile in one and distance in the other but on the same index so we can sort
        List<GameObject> tiles = new List<GameObject>();
        List<float> distances = new List<float>();

        //Loop through all the tiles and add the tile and distances to their lists
        foreach (GameObject tile in randomOrder)
        {
            float distance = Vector3.Distance(tile.transform.position, startPos);
            tiles.Add(tile);
            distances.Add(distance);
        }

        //Get the amount of tiles before we begin removing from the list
        int tileAmount = tiles.Count;

        //Loop through all tiles, get the max distance that is in the tiles and add it to our final list,
        //then remove the tile and distance from their list (Until no more values are there and all is sorted)
        for(int i = 0; i < tileAmount; i++)
        {
            float maxDistance = Mathf.Max(distances.ToArray());
            int index = distances.IndexOf(maxDistance);
            GameObject tile = tiles[index];

            tileOrderedOnDistance.Add(tile);

            tiles.RemoveAt(index);
            distances.RemoveAt(index);
        }
    }
    IEnumerator DestroyFurthestPossibleObject()
    {
        //Checks if we have more then 1 tile left
        if (tileOrderedOnDistance.Count > 2)
        {
            //Wait out specified delay
            yield return new WaitForSeconds(tileDestroyDelay);

            //Makes variables with the targets details
            GameObject target = tileOrderedOnDistance[0];
            TryDestroy(target);
            StartCoroutine("DestroyFurthestPossibleObject");
        }
        else
        {
            //Lost the game
            LostGame();
        }
    }
    void TryDestroy(GameObject obj)
    {
        //If there is only a tile
        if (CheckForTileOnly(obj.transform.position))
        {
            Destroy(obj);
            tileOrderedOnDistance.Remove(obj);
        }
        //If there is more than a tile
        else
        {
            //Check if there is a removable object here
            GameObject removable = CheckForRemovable(obj.transform.position);
            if (removable != null)
            {
                Destroy(removable);
            }
            //Check if the player is here
            else if(CheckForPlayer(obj.transform.position))
            {
                TryDestroy(tileOrderedOnDistance[1]);
            }
            else
            {
                Debug.LogError("This code should not be reached!");
            }
        }
    }

    bool CheckForPlayer(Vector3 pos)
    {
        //Loop through everything on this tile
        foreach (RaycastHit2D thing in WhatIsAt(pos))
        {
            //if it is a player tag return true
            if (thing.collider.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    bool CheckForTileOnly(Vector3 pos)
    {
        //If there is only one thing at this position, it will be a tile
        if (WhatIsAt(pos).Length > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    GameObject CheckForRemovable(Vector3 pos)
    {
        //Loop through everything on this tile
        foreach (RaycastHit2D thing in WhatIsAt(pos))
        {
            //if it is a immovable or a pushable return it
            if(thing.collider.tag == "Immovable" || thing.collider.tag == "Pushable")
            {
                return thing.collider.gameObject;
            }
        }
        //If it gets here then there is no immovable or pushable at this position
        return null;
    }

    //Checks for all the colliders in a given position and returns a raycasthit2d array with them
    //If there is nothing returns null
    public RaycastHit2D[] WhatIsAt(Vector3 pos)
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(pos, new Vector2(0.5f, 0.5f), 0f, transform.forward, 10f);
        if (hit.Length > 0)
        {
            return hit;
        }
        else
        {
            return null;
        }

    }

    void LostGame()
    {
        //Load lose scene
    }
}
