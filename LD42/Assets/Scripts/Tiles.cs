using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    private List<GameObject> tileOrderedOnDistance = new List<GameObject>();
    public float tileDestroyDelay;

    // Use this for initialization
    void Start () {
        //Start of game initialize the ordered array on distance
        CreateTileIndex();
        StartCoroutine("DestroyRandomTile");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Populates tileOrderedOnDistance list
    //TODO: There is probably a better way of doing this!
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
            float distance = Vector3.Distance(tile.transform.position, Vector3.zero);
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

            tileOrderedOnDistance.Add(tiles[index]);

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
            Destroy(target);
            tileOrderedOnDistance.Remove(target);

            StartCoroutine("DestroyRandomTile");
        }
        else
        {
            Debug.Log("No more tiles left");
            //TODO: We won?
        }
    }
}
