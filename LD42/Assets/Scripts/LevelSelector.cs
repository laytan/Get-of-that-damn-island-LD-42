using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public GameObject currentLvl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick(int index)
    {
        index--;
        currentLvl.GetComponent<CurrentLevel>().lvl = index;
        SceneManager.LoadScene(0);
    }
}
