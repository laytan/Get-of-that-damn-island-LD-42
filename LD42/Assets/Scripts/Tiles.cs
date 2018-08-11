using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            StartCoroutine("DestroyRandomTile");
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

    //Waits for the tileDestroyDelay and destroy's the tile that's furthest away from the middle
    IEnumerator DestroyRandomTile()
    {
        if (tileOrderedOnDistance.Count > 1)
        {
            yield return new WaitForSeconds(tileDestroyDelay);
            GameObject target = tileOrderedOnDistance[0];
            Vector3 posToRemove = target.transform.position;
            //Checks if this tile was populated, if so, remove the populator
            if (WhatIsAt(posToRemove).Length > 1)
            {
                foreach(RaycastHit2D thing in WhatIsAt(posToRemove))
                {
                    if(thing.collider.tag != "Tile")
                    {
                        Destroy(thing.collider.gameObject);
                    }
                }
            } 
            else
            {
                Destroy(target);
                tileOrderedOnDistance.Remove(target);
            }
            //Loop back
            StartCoroutine("DestroyRandomTile");
        }
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
}
