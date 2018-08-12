using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public Animation anim1;
    private UI ui;
    private SoundManager sm;
    Vector3 pos;
    bool play = false;
    public AudioClip takeOff;
    public AudioClip won;
	// Use this for initialization
	void Start () {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {    
		if(play)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 2.5f);
        }
	}
    public IEnumerator PlayEndSequence()
    {
        yield return new WaitForSeconds(2);
        anim1.Play();
        sm.PlaySound(takeOff);
        pos = transform.position + new Vector3(0, 20, 0);
        play = true;
        Invoke("ToWinScreen", 3.5f);
    }
    void ToWinScreen()
    {
        ui.ToWinScreen();
    }
}
