using UnityEngine;
using System.Collections;
using System;

public class NPCGenericMovement : MonoBehaviour {

	
	public float moveSpeed = 0.02f;

	
	[SerializeField]
	private short fieldOfViewFront = 3;
	[SerializeField]
	private float fieldOfViewSides = 0.3f;
	
	[SerializeField]
	protected short moveCount = 0;
	[SerializeField]
	protected short frameCount = 180;
	
	private Animator animator;
	
	private bool movingRight = false;
	private bool movingLeft = false;
	private bool movingUp = false;
	private bool touchingUp = false;
	private bool touchingBot = false;
	private bool touchingLeft = false;
	private bool touchingRight = false;
	
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

			short optPossibilities = checkBorders(transform.position);
			
			short opt = 0;
			bool passedTest = false;
			
			while(!passedTest){
				opt = (short) Mathf.Ceil((rand * optPossibilities));
				passedTest = validateOptionAgainstCheckers(opt);
			}
			
			if (opt == 1){
				//UP
				movingUp = true;
				movingRight = false;
				movingLeft = false;
			}
			else if (opt == 2){
				//RIGHT
				movingUp = false;
				movingRight = true;
				movingLeft = false;
				
			}
			else if (opt == 3){
				//BOT
				movingUp = false;
				movingRight = false;
				movingLeft = false;
				
			}
			else if (opt == 4){
				//LEFT
				movingUp = false;
				movingRight = false;
				movingLeft = true;
				
			}
		}
		
		if (moveCount < frameCount){
			moveCount ++;
			checkBorders(transform.position);
			moveCharacter (movingUp,movingRight,movingLeft);
		}
		
		else {
			moveCount = 0;
			unflagAllTouchChecks();
		}
	}
	
	public void moveCharacter(bool up, bool right, bool left){
		bool done = false;
		while(!done){
			if (up) {
				if (!touchingUp){
					activateAnimation("isWalkingUp");			
					transform.position += new Vector3(0,moveSpeed,0);
					done = true;
				}
				else {
					movingUp = false;
					up = false;
				}
			}
			else if (right){
				if (!touchingRight){
					activateAnimation("isWalkingRight");			
					transform.position += new Vector3(moveSpeed,0,0);
					done = true;
				}
				else {
					movingRight = false;
					movingLeft = true;
					right = false;
					left = true;
					
				}
			}
			else if (left){
				if (!touchingLeft){
					activateAnimation("isWalkingLeft");		
					transform.position += new Vector3(-(moveSpeed),0,0);
					done = true;
				}
				else {
					movingLeft = false;
					movingRight = true;
					left = false;
					right = true;
				}
			}
			else {
				if (!touchingBot){
					activateAnimation("isWalkingBot");
					transform.position += new Vector3(0,-(moveSpeed),0);
					done = true;
				}
				else {
					movingUp = true;
					up = true;
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
		if (opt == 1 && touchingUp)
			return false;
		else if (opt == 2 && touchingRight)
			return false;
		else if (opt == 3 && touchingBot)
			return false;
		else if (opt == 4 && touchingLeft)
			return false;
		else 
			return true;
	}
	
	// HIC SUNT DRACONES
	private short checkBorders(Vector3 checkVector)
	{
		// 1 - Main variables
		short movePossibilities = 4;
		Vector3 newPosition = checkVector;
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		// 2 - X Axis variables
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		
		// 3 - Y Axis variables
		float yDist = mainCamera.orthographicSize;
		float yMax = mainCamera.transform.position.y + yDist;
		float yMin = mainCamera.transform.position.y - yDist;
		
		
		// 4 - Touching Check
		
		// Check for X Axis
		if (newPosition.x <= xMin + 0.3f) {
			movePossibilities --;
			touchingLeft = true;
			
		}
		else if(newPosition.x >= xMax - 0.3f){
			movePossibilities --;
			touchingRight = true;
		}
		
		// Check for Y Axis
		if(newPosition.y <= yMin + 0.3f){
			movePossibilities --;
			touchingBot = true;
			
		}
		else if(newPosition.y >= yMax - 0.3f){
			movePossibilities --;
			touchingUp = true;
		}
		return movePossibilities;
	}
	
	private void unflagAllTouchChecks(){
		touchingUp = false;
		touchingRight = false;
		touchingBot = false;
		touchingLeft = false;
	}

	protected void lookFor(Vector3 objectivePosition){
		
		Debug.Log (objectivePosition);
		
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
		
		else {
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
			
		}
		
		else if (movingLeft){
			movingLeft = false;
			int rand = getRandInt();
			
			if (rand == 0){
				movingUp = true;
			}
		}
		else {
			int rand = getRandInt();
			
			if (rand == 0){
				movingRight = true;
			}
			else{
				movingLeft = true;
			}
		}

	}
	
	private int getRandInt(){
		int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
		System.Random random = new System.Random(seed);
		return random.Next(0,2);
	}

}
