using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {

    private bool isEating = false;
    private float time = 0;
	
	void Start(){
		initializeAnimator(gameObject.GetComponent<Animator>());
	}

	void Update(){
		applyMotion();
		lookFor(GameObject.Find("player").transform.position);
	}

	public void eatDonut(){
		isEating = true;
		while (time < 2)
		{
			time += Time.deltaTime;
			
		}
		isEating = false;
		time = 0;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name.CompareTo("player") == 1){
		}
		else {
			onCollisionChangeDirection();
		}
	}
}