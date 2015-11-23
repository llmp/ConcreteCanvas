using UnityEngine;
using System.Collections;
using System;

public class PainterMovement : NPCGenericMovement {
	
	// Use this for initialization
	void Start () {
		initializeAnimator(gameObject.GetComponent<Animator>());
	}
	
	// Update is called once per frame
	void Update () {
		applyMotion();
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("Painter Collision: " + collider.name); 
		if (collider.name.CompareTo("player") == 0){
			GameObject.Find("cop").GetComponent<CopMovement>().foundSomething();
		}
		onCollisionChangeDirection();
	}

	void OnTriggerExit2D(Collider2D collider){
//		Debug.Log ("No longer colliding with " + collider.name);
	}
}
