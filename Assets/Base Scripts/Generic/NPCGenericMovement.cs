using UnityEngine;
using System.Collections;
using System;

public class NPCGenericMovement : MonoBehaviour {
	
	[SerializeField]
	protected short fieldOfViewFront = 3;
	[SerializeField]
	protected float fieldOfViewSides = 0.3f;
	[SerializeField]
	private float moveSpeed = 0.02f;
	[SerializeField]
	protected short moveCount = 0;
	[SerializeField]
	protected short frameCount = 180;

	protected bool isChasing = false;
	private bool movingRight = false;
	private bool movingLeft = false;
	private bool movingUp = false;
	private bool movingBot = false;
	private Animator animator;
    public bool autoMotion = true;

    // Use this for initialization
    void Start () {

	}
	
	protected void initializeAnimator(Animator childAnimator){
		this.animator = childAnimator;
	}

	// Update is called once per frame
	public void applyMotion() {

		if (moveCount == 0){

			int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
			System.Random random = new System.Random(seed);
			float rand = (float) random.NextDouble();

			short optPossibilities = gameObject.GetComponent<BoundariesChecker>().getPossibleMoves(transform.position);
			
			short opt = 0;
			bool passedTest = false;
			
			while(!passedTest){
				opt = (short) Mathf.Ceil(rand * optPossibilities);
				passedTest = validateOptionAgainstCheckers(opt);
			}
			
			if (opt == 1){
				movingUp = true;
				movingRight = false;
				movingLeft = false;
				movingBot = false;
			}
			else if (opt == 2){
				movingUp = false;
				movingRight = true;
				movingLeft = false;
				movingBot = false;
				
			}
			else if (opt == 3){
				movingUp = false;
				movingRight = false;
				movingLeft = false;
				movingBot = true;
			}
			else if (opt == 4){
				movingUp = false;
				movingRight = false;
				movingLeft = true;
				movingBot = false;
				
			}
		}
		
		if (moveCount < frameCount){
			moveCount ++;
			gameObject.GetComponent<BoundariesChecker>().checkBorders(transform.position);
			moveCharacter ();
		}
		
		else {
			moveCount = 0;
			gameObject.GetComponent<BoundariesChecker>().unflagAllTouchChecks();
		}
	}

    public void moveCharacter(){
		bool done = false;
		while(!done){
			if (movingUp) {
				if (!gameObject.GetComponent<BoundariesChecker>().isTouchingTop){
					activateAnimation("isWalkingUp");			
					transform.position += new Vector3(0,moveSpeed,0);
					done = true;
				}
				else {
					movingUp = false;
					movingBot = true;
				}
			}
			else if (movingRight){
				if (!gameObject.GetComponent<BoundariesChecker>().isTouchingRight){
					activateAnimation("isWalkingRight");			
					transform.position += new Vector3(moveSpeed,0,0);
					done = true;
				}
				else {
					movingRight = false;
					movingLeft = true;
				}
			}
			else if (movingLeft){
				if (!gameObject.GetComponent<BoundariesChecker>().isTouchingLeft){
					activateAnimation("isWalkingLeft");		
					transform.position += new Vector3(-(moveSpeed),0,0);
					done = true;
				}
				else {
					movingLeft = false;
					movingRight = true;
				}
			}
			else if (movingBot){
				if (!gameObject.GetComponent<BoundariesChecker>().isTouchingBottom){
					activateAnimation("isWalkingBot");
					transform.position += new Vector3(0,-(moveSpeed),0);
					done = true;
				}
				else {
					movingUp = true;
					movingBot = false;
				}
			}
		}
	}
	
	private void activateAnimation(string animationStr){
		animator.SetBool("isWalkingUp",false);
		animator.SetBool("isWalkingRight",false);
		animator.SetBool("isWalkingLeft",false);
		animator.SetBool("isWalkingBot",false);
		animator.SetBool(animationStr,true);
	}
	
	private bool validateOptionAgainstCheckers(short opt){
		if (opt == 1 && gameObject.GetComponent<BoundariesChecker>().isTouchingTop)
			return false;
		else if (opt == 2 && gameObject.GetComponent<BoundariesChecker>().isTouchingRight)
			return false;
		else if (opt == 3 && gameObject.GetComponent<BoundariesChecker>().isTouchingBottom)
			return false;
		else if (opt == 4 && gameObject.GetComponent<BoundariesChecker>().isTouchingLeft)
			return false;
		else 
			return true;
	}

	protected void lookFor(GameObject objective){

		Vector3 objectivePosition = objective.transform.position;

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
		
		else if (movingBot){
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
 		this.isChasing = true;
		if (!controller.GetComponent<PrefabFactory>().instanceExists){
			controller.GetComponent<PrefabFactory>()
				.instantiatePrefabBornToDie(controller.GetComponent<PrefabFactory>().prefabArray[0],
				                            instantiateAt,1.5f);
		}
	}

	protected void onCollisionChangeDirection(){
		/*
		 * As the character shouldn't walk back the direction he was originally coming from, 
		 * we'll generate a random number between 0 and 1 to decide which of the remaining 
		 * 2 directions he will move for the next frames
		 */
		
		if (movingUp){
			//Block current walking direction
			movingUp = false;
			//Get a random number
			int rand = getRandInt();
			
			if (rand == 0){
				movingRight = true;
			}
			else{
				movingLeft = true;
			}
		}
		
		else if (movingRight){
			movingRight = false;
			int rand = getRandInt();
			
			if (rand == 0){
				movingUp = true;
			}
			else {
				movingBot = true;
			}
		}
		
		else if (movingLeft){
			movingLeft = false;
			int rand = getRandInt();
			
			if (rand == 0){
				movingUp = true;
			}
			else {
				movingBot = true;
			}
		}

		else if (movingBot){
			int rand = getRandInt();
			
			if (rand == 0){
				movingRight = true;
			}
			else{
				movingLeft = true;
			}
		}
	}

	protected void gotoPosition(Vector3 objectivePosition){
		Vector3 npcPosition = this.transform.position;

		float factor = 0.3f;
//		if (npcPosition.x != objectivePosition.x){
			if (npcPosition.x + factor < objectivePosition.x){
			unflagAllMovementDirections();
				movingRight = true;
			}
			
			if (npcPosition.x - factor > objectivePosition.x){
			unflagAllMovementDirections();
				movingLeft = true;
			}
//		}
//		if ( && npcPosition.y - factor > objectivePosition.y){
			if (npcPosition.y +factor < objectivePosition.y){
			unflagAllMovementDirections();
				movingUp = true;
			}

			if (npcPosition.y - factor > objectivePosition.y){
			unflagAllMovementDirections();
				movingBot = true;
			}
//		}
		moveCharacter();
	}

	private void unflagAllMovementDirections(){
		this.movingUp = false;
		this.movingRight = false;
		this.movingLeft = false;
	}

	public void setMovement(bool status)
    {
        autoMotion = status;
	}

	private int getRandInt(){
		int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
		System.Random random = new System.Random(seed);
		return random.Next(0,2);
	}

}
