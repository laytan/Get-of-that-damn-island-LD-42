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
    //If we collide with a pushable object we get the position we want to push it to,
    //and then run moveTo on the objects script
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GameObject target = collision.gameObject;
            Vector3 pushablePos = target.transform.position + transform.up;
            
            target.GetComponent<Pushable>().MoveTo(pushablePos);
        }
    }
    //If we are colliding let our movement script know so we arent able to move through the pushable object
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GetComponent<CharacterMovement>().areColliding = true;
        }
    }
    //Make the player able to move again
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            GetComponent<CharacterMovement>().areColliding = false;
        }
    }
}
