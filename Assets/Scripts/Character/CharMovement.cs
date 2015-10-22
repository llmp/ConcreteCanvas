using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {
	
	public SpriteRenderer spriteRenderer;
	public float moveSpeed = 0.5f;
	public float jumpHeight = 5.0f;
	private bool isFacingRight = true;
	
	//	public ChangeSprite changeSprite = new ChangeSprite();
	
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer.sprite == null) {
			spriteRenderer.sprite = gameObject.GetComponent<ChangeSprite>().falling;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.D)) {
			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().right);
			if (!isFacingRight){
				flip();
			}
			isFacingRight = true;
			transform.position += new Vector3(moveSpeed,0,0);
		} 
		else if (Input.GetKey (KeyCode.A)) {
			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().left);
			if (isFacingRight){
				flip ();
			}
			isFacingRight = false;
			transform.position += new Vector3(-(moveSpeed),0,0);
		} 
		else if (Input.GetKey(KeyCode.W)) {
			
			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().up);
			transform.position += new Vector3(0,moveSpeed,0);
			
		} 
		else if (Input.GetKey(KeyCode.S)){
			gameObject.GetComponent<ChangeSprite>().ChangeSpriteTo(spriteRenderer, gameObject.GetComponent<ChangeSprite>().down);
			transform.position += new Vector3(0,-(moveSpeed),0);
		}
	}
	
	private void flip(){
		gameObject.transform.localScale = new Vector3((gameObject.transform.localScale.x * (-1)), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}
	
}