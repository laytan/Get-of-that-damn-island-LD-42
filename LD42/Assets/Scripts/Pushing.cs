using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour {


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GameObject target = collision.gameObject;
            Vector3 pushablePos = target.transform.position + transform.up;
            
            target.GetComponent<Pushable>().MoveTo(pushablePos);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GetComponent<CharacterMovement>().areColliding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GetComponent<CharacterMovement>().areColliding = false;
        }
    }
}
