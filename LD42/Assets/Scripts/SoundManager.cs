using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource audioS;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaySound(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
