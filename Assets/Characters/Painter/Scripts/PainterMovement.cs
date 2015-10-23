using UnityEngine;
using System.Collections;
using System;

public class PainterMovement : MonoBehaviour {

	//	public SpriteRenderer spriteRenderer;
	public float moveSpeed = 0.02f;
	
	[SerializeField]
	private short moveCount = 0;
	[SerializeField]
	private short frameCount = 180;
	
	private Animator animator;
	
	private bool movingRight = false;
	private bool movingLeft = false;
	private bool movingUp = false;	
	private bool touchingUp = false;
	private bool touchingBot = false;
	private bool touchingLeft = false;
	private bool touchingRight = false;
	
	//	public ChangeSprite changeSprite = new ChangeSprite();
	
	// Use this for initialization
	void Start () {
		//		spriteRenderer = GetComponent<SpriteRenderer> ();
		//		if (spriteRenderer.sprite == null) {
		//			spriteRenderer.sprite = gameObject.GetComponent<ChangeSprite>().falling;
		//		}
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Apply Reinforced Learning
		if (moveCount == 0){
			
			int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
			System.Random random = new System.Random(seed);
			float rand = (float) random.NextDouble();
			
			
			//Check touchingTop,touchingBot,touchingLeft,touchingRight to set optPossibilities
			short optPossibilities = checkBorders(transform.position);
			
			short opt = 0;
			bool passedTest = false;
			
			while(!passedTest){
				opt = (short) Mathf.Ceil((rand * optPossibilities));
				passedTest = validateOptionAgainstCheckers(opt);
				//				Debug.Log("Validate:" + passedTest);
				//				Debug.Log("TouchUp:" + touchingUp + ",TouchRight:" + touchingRight + ",TouchBot:" + touchingBot + ",TouchLeft:" + touchingLeft);
			}
			Debug.Log (opt);
			
			if (opt == 1){
				//UP
				movingUp = true;
				movingRight = false;
				movingLeft = false;
				//				Debug.Log ("UP:" + movingUp + ",RIGHT:" + movingRight + "LEFT:" + movingLeft);
			}
			else if (opt == 2){
				//RIGHT
				movingUp = false;
				movingRight = true;
				movingLeft = false;
				//				Debug.Log ("UP:" + movingUp + ",RIGHT:" + movingRight + "LEFT:" + movingLeft);
			}
			else if (opt == 3){
				//BOT
				movingUp = false;
				movingRight = false;
				movingLeft = false;
				//				Debug.Log ("UP:" + movingUp + ",RIGHT:" + movingRight + "LEFT:" + movingLeft);
			}
			else if (opt == 4){
				//LEFT
				movingUp = false;
				movingRight = false;
				movingLeft = true;
				//				Debug.Log ("UP:" + movingUp + ",RIGHT:" + movingRight + "LEFT:" + movingLeft);
			}
		}
		
		if (moveCount < frameCount){
			moveCount ++;
			checkBorders(transform.position);
			moveCop (movingUp,movingRight,movingLeft);
		}
		
		else {
			moveCount = 0;
			//Unflag all touchValidators here
			unflagAllTouchChecks();
		}
	}
	
	public void moveCop(bool up, bool right, bool left){
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
		
		//		Debug.Log( "xDist:" + xDist + ",xMax:" + xMax + ",xMin:" + xMin + ",yDist:" + yDist + ",yMax:" + yMax + ",yMin:" + yMin 
		//		          + ",CurrentPosition:" + newPosition.x + "  " + newPosition.y);
		
		// 4 - Touching Check
		
		// Check for X Axis
		if (newPosition.x <= xMin + 0.3f) {
			movePossibilities --;
			touchingLeft = true;
			//			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			//			moveDirection.x = -moveDirection.x;
		}
		else if(newPosition.x >= xMax - 0.3f){
			movePossibilities --;
			touchingRight = true;
		}
		
		// Check for Y Axis
		if(newPosition.y <= yMin + 0.3f){
			movePossibilities --;
			touchingBot = true;
			//			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
			//			moveDirection.y = -moveDirection.y;
		}
		else if(newPosition.y >= yMax - 0.3f){
			movePossibilities --;
			touchingUp = true;
		}
		//		transform.position = newPosition;
		return movePossibilities;
	}
	
	private void unflagAllTouchChecks(){
		touchingUp = false;
		touchingRight = false;
		touchingBot = false;
		touchingLeft = false;
	}

}
