using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour {

    GameObject target;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -10);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        }
	}

    public void FollowMe(GameObject go)
    {
        target = go;
    }
}
