using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D[] maps;
    public ColorToPrefab[] colorMappings;
    public GameObject Tiles;
    Tiles tiles;

    public int currentLvl;
	// Use this for initialization
	void Start () {
        currentLvl = GameObject.FindGameObjectWithTag("Lvl").GetComponent<CurrentLevel>().lvl;

        tiles = Tiles.GetComponent<Tiles>();
        GenerateLevel();
        tiles.LevelIsGenerated();
	}

    void GenerateLevel()
    {
        for(int x = 0; x < maps[currentLvl].width; x++)
        {
            for(int y = 0; y < maps[currentLvl].height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = maps[currentLvl].GetPixel(x, y);
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
