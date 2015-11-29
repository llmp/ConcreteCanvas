using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
    private bool standingUp = false;
    private bool standingDown = false;
    private bool standingLeft = false;
    private bool standingRight = false;
    private bool touchingWall = false;
    private bool touchingDonut = false;
    private bool touchingSkate = false;
	private bool isWalking = false;
	private float moveSpeed = 0.02f;
	private Animator animator;

	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}

	void Update () {
		getKeyDown();
		getFacingDirection();
		getKeyUp();
		getInteractionKey();
    }

	private void getKeyDown(){
		if (Input.GetKey (KeyCode.D)) {
			if (!isWalking){
				activateAnimation("isWalkingRight");
				isWalking = true;
			}
			transform.position += new Vector3(moveSpeed,0,0);
		} 
		else if (Input.GetKey (KeyCode.A)) {
			if (!isWalking){
				activateAnimation("isWalkingLeft");
				isWalking = true;
			}
			transform.position += new Vector3(-(moveSpeed),0,0);
		} 
		else if (Input.GetKey(KeyCode.W)) {
			if (!isWalking){
				activateAnimation("isWalkingUp");
				isWalking = true;
			}
			transform.position += new Vector3(0,moveSpeed,0);
			
		} 
		else if (Input.GetKey(KeyCode.S)){
			if (!isWalking){
				activateAnimation("isWalkingBot");
				isWalking = true;
			}
			transform.position += new Vector3(0,-(moveSpeed),0);
		}
	}

	private void getKeyUp(){
		if (Input.GetKeyUp(KeyCode.A))
		{
			activateAnimation("isStanding");
			setDirection("left");
			setWalking(false);
		}
		
		else if (Input.GetKeyUp(KeyCode.S))
		{
			activateAnimation("isStanding");
			setDirection("down");
			setWalking(false);
		}
		
		else if (Input.GetKeyUp(KeyCode.D))
		{
			activateAnimation("isStanding");
			setDirection("right");
			setWalking(false);
		}
		
		else if (Input.GetKeyUp(KeyCode.W))
		{
			activateAnimation("isStanding");
			setDirection("up");
			setWalking(false);
		}
	}

	private void getInteractionKey(){
		if ((Input.GetKey(KeyCode.Space)) )
		{
			if (touchingWall && standingUp)
			{
				interact("wall");
			}
			else if (touchingDonut)
			{
				interact("donut");
			}
            else if (touchingSkate)
            {
                interact("skateboard");
            }

        }
	}

	private void activateAnimation(string animationStr){
		animator.SetBool("isWalkingUp",false);
		animator.SetBool("isWalkingRight",false);
		animator.SetBool("isWalkingLeft",false);
		animator.SetBool("isWalkingBot",false);
		animator.SetBool("isStanding",false);
		animator.SetBool(animationStr,true);
	}

    public void setDirection(string direction){
		if (direction == "up")
        {
            standingUp = true;
            standingRight = false;
            standingLeft = false;
            standingDown = false;
        }
		else if (direction == "left")
        {
            standingUp = false;
            standingRight = false;
            standingLeft = true;
            standingDown = false;
        }
		else if (direction == "down")
        {
            standingUp = false;
            standingRight = false;
            standingLeft = false;
            standingDown = true;
        }
		else if (direction == "right")
        {

            standingUp = false;
            standingRight = true;
            standingLeft = false;
            standingDown = false;
        }
    }
    public void setWalking(bool isWalking){
        this.isWalking = isWalking;
    }

    public int getFacingDirection(){
        if (standingDown)
        {
            return 1;
        }
        else if (standingLeft)
        {
            return 2;
        }
        else if (standingRight)
        {
            return 3;
        }
        else if (standingUp)
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }
    void paint()
    {

    }

	private void interact(string str){
		if (str == "wall")
		{
			paint();
		} 
		else if(str == "donut")
		{
            
            GameObject.Destroy(GameObject.Find("donut"));
            touchingDonut = false;
            GameObject.Find("cop").GetComponent<CopMovement>().eatDonut();

        }
        else if(str == "skateboard")
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed *= 3;
            Debug.Log("SKATE");
            GameObject.Destroy(GameObject.Find("skateboard"));
            touchingSkate = false;
        }
        else if(str == "paintBucket")
        {

        }
       
        else
        {
            Debug.Log("Error");
        }

	}

    void pickUpItem(){
//       GameObject obj= GameObject.Find("cop");
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
      if (collision.name == "wall")
      {
         touchingWall = true;
      }else if(collision.name == "donut")
      {
            touchingDonut= true;
      }
        else if (collision.name == "skateboard")
        {
            touchingSkate = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision){

        if (collision.name == "wall")

        {
            touchingWall = false;
        }
        else if (collision.name == "donut")
        {
            touchingDonut = false;
        }
        else if (collision.name == "skateboard")
        {
            touchingSkate = false;
        }
    }

}