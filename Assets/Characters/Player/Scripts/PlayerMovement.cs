using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public SpriteRenderer spriteRenderer;
	public float moveSpeed = 0.5f;
	public float jumpHeight = 5.0f;

	private bool isWalking = false;
	private Animator animator;
	
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
		if (Input.GetKey (KeyCode.D)) {

//			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().right);
//			if (!isFacingRight){
//				flip();
//			}
//			isFacingRight = true;
			if (!isWalking){
				activateAnimation("isWalkingRight");
				isWalking = true;
			}
			transform.position += new Vector3(moveSpeed,0,0);
		} 
		else if (Input.GetKey (KeyCode.A)) {
//			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().left);
//			if (isFacingRight){
//				flip ();
//			}
//			isFacingRight = false;
			if (!isWalking){
				activateAnimation("isWalkingLeft");
				isWalking = true;
			}
			transform.position += new Vector3(-(moveSpeed),0,0);
		} 
		else if (Input.GetKey(KeyCode.W)) {
//			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().up);
			if (!isWalking){
				activateAnimation("isWalkingUp");
				isWalking = true;
			}
			transform.position += new Vector3(0,moveSpeed,0);
			
		} 
		else if (Input.GetKey(KeyCode.S)){
//			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().down);
			if (!isWalking){
				activateAnimation("isWalkingBot");
				isWalking = true;
			}
			transform.position += new Vector3(0,-(moveSpeed),0);
		}
		else if (Input.GetKey(KeyCode.Space)){
			
		}

		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)){
            
			activateAnimation("isStanding");
			isWalking = false;
		}
		//		if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)){
		//			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().neutral);
		//		}
	}
	
	private void flip(){
		gameObject.transform.localScale = new Vector3((gameObject.transform.localScale.x * (-1)), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}

	private void activateAnimation(string animationStr){
		animator.SetBool("isWalkingUp",false);
		animator.SetBool("isWalkingRight",false);
		animator.SetBool("isWalkingLeft",false);
		animator.SetBool("isWalkingBot",false);
		animator.SetBool("isStanding",false);
		animator.SetBool(animationStr,true);
	}

}