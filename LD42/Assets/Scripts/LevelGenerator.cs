using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public GameObject Tiles;
    Tiles tiles;
	// Use this for initialization
	void Start () {
        tiles = Tiles.GetComponent<Tiles>();
        GenerateLevel();
        tiles.LevelIsGenerated();
	}

    void GenerateLevel()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if(pixelColor.a == 0)
        {
            //transparent
            return;
        }
        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector2 pos = new Vector2(x, y);

                if(colorMapping.foundation != null)
                {
                    Instantiate(colorMapping.foundation, pos, Quaternion.identity, Tiles.transform);
                }
                Instantiate(colorMapping.prefab, pos, Quaternion.identity, Tiles.transform);
               
            }
        }
    }
}
