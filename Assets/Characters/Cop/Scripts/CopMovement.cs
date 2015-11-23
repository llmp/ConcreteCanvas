using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {
    //states

    private bool eating = false;
    private float time = 0;


	// Use this for initialization
	void Start () {
		initializeAnimator(gameObject.GetComponent<Animator>());
	}
	
	// Update is called once per frame
	void Update () {
		applyMotion();
		lookFor(GameObject.Find("player").transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name.CompareTo("player") == 1){
		}
		else {
			onCollisionChangeDirection();
		}
	}
    public void toEat()
    {
        eating = true;
        while (time < 2)
        {
            time += Time.deltaTime;

        }
        eating = false;
        time = 0;
    }
}