using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {

	private Animator animator;

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
		Debug.Log ("Cop Collision: " + collider.name);
		if (collider.name.CompareTo("player") == 1){
		}
		else {
			onCollisionChangeDirection();
		}
	}

}