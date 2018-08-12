using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Tiles").GetComponent<Tiles>().SetStartPos(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "target(Clone)")
        {
            WonGame();
        }
    }
    void WonGame()
    {
        SceneManager.LoadScene(3);
    }
   

}
