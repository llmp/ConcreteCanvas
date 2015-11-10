using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {


	[SerializeField]
	private short fieldOfViewFront = 3;
	[SerializeField]
	private float fieldOfViewSides = 0.3f;

	private Animator animator;

	private bool movingRight = false;
	private bool movingLeft = false;
	private bool movingUp = false;
	private bool movingBot = false;
	private bool touchingUp = false;
	private bool touchingBot = false;
	private bool touchingLeft = false;
	private bool touchingRight = false;


	// Use this for initialization
	void Start () {
		initializeAnimator(gameObject.GetComponent<Animator>());
	}
	
	// Update is called once per frame
	void Update () {
		applyMotion();
		lookFor(GameObject.Find("player").transform.position);
	}
	
	private void lookFor(Vector3 objectivePosition){

		float yViewMin;
		float yViewMax;
		float xViewMin;
		float xViewMax;
		Vector3 currentPosition = transform.position;

		if(movingRight){
			yViewMin = currentPosition.y - fieldOfViewSides;
			yViewMax = currentPosition.y + fieldOfViewSides;
			xViewMin = currentPosition.x;
			xViewMax = currentPosition.x + fieldOfViewFront;
			
			if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax){
				foundSomething();
			}

		}

		else if(movingLeft){
			yViewMin = currentPosition.y - fieldOfViewSides;
			yViewMax = currentPosition.y + fieldOfViewSides;
			xViewMin = currentPosition.x;
			xViewMax = currentPosition.x - fieldOfViewFront;
			
			if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x <= xViewMin && objectivePosition.x >= xViewMax){
				foundSomething();
			}
		}

		else if(movingUp){
			yViewMin = currentPosition.y;
			yViewMax = currentPosition.y + fieldOfViewFront;
			xViewMin = currentPosition.x  - fieldOfViewSides;
			xViewMax = currentPosition.x + fieldOfViewSides;
			
			if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax){
				foundSomething();
			}
		}

		else if(movingBot){
			yViewMin = currentPosition.y;
			yViewMax = currentPosition.y - fieldOfViewFront;
			xViewMin = currentPosition.x - fieldOfViewSides;
			xViewMax = currentPosition.x + fieldOfViewSides;
			
			if (objectivePosition.y >= yViewMax && objectivePosition.y <= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax){
				foundSomething();
			}
		}
	}

	public void foundSomething(){
		Vector3 instantiateAt = transform.position + new Vector3(0.2f,0.6f,0);
		GameObject controller = GameObject.Find("controller");
		if (!controller.GetComponent<PrefabFactory>().instanceExists){
			controller.GetComponent<PrefabFactory>()
				.instantiatePrefabBornToDie(controller.GetComponent<PrefabFactory>().prefabArray[0],
			    	                       instantiateAt,1.5f);
		}
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