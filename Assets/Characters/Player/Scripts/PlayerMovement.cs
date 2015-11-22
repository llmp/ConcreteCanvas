using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    //face states
    private bool standingUp = false;
    private bool standingDown = false;
    private bool standingLeft = false;
    private bool standingRight = false;

    //interactions
    private bool touchingWall = false;


    public SpriteRenderer spriteRenderer;
	public float moveSpeed = 0.5f;
	public float jumpHeight = 5.0f;

	private bool isWalking = false;
	private Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

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
		else if (Input.GetKey(KeyCode.Space)){
			
		}

        /*	if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W)){
                activateAnimation("isStanding");
                isWalking = false;
            }
            */

        //possible use of switch case keycode.variable case variable A,S...
        if (Input.GetKeyUp(KeyCode.A))
        {
            activateAnimation("isStanding");
            setDirection("left");
            setWalk(false);
            //isWalking = false;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            activateAnimation("isStanding");
            setDirection("down");
            setWalk(false);
            //isWalking = false;

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            activateAnimation("isStanding");
            setDirection("right");
            setWalk(false);
            //isWalking = false;

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            activateAnimation("isStanding");
            setDirection("up");
            setWalk(false);
            //isWalking = false;


        }


        if ((Input.GetKey(KeyCode.Space)) /*&& touch*/)
        {
            interact();
            
        }
        getFace();
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


    //testing the idea in the same script
    //aim to referr to another object

    private void interact()
    {
        Debug.Log("Touched");
    }

    public void setDirection(string dir)
    {
        if (dir == "up")
        {
            standingUp = true;
            standingRight = false;
            standingLeft = false;
            standingDown = false;
        }
        else if (dir == "left")
        {

            standingUp = false;
            standingRight = false;
            standingLeft = true;
            standingDown = false;
        }
        else if (dir == "down")
        {

            standingUp = false;
            standingRight = false;
            standingLeft = false;
            standingDown = true;
        }
        else if (dir == "right")
        {

            standingUp = false;
            standingRight = true;
            standingLeft = false;
            standingDown = false;
        }
        else
        {
            Debug.Log("Wrong string");
        }


    }
    public void setWalk(bool b)
    {
        isWalking = b;
    }

    public int getFace()
    {
        if (standingDown)
        {
            Debug.Log("down");
            return 1;
        }
        else if (standingLeft)
        {
            Debug.Log("left");
            return 2;
        }
        else if (standingRight)
        {
            Debug.Log("right");
            return 3;
        }
        else if (standingUp)
        {
            Debug.Log("up");
            return 4;
        }
        else
        {
            Debug.Log("no direction");
            return 0;
        }



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
      if (collision.name == "wall")
            
        {
        touchingWall = true;
            Debug.Log(touchingWall + "TW");
        //collision.gameObject.SendMessage("in front of the wall")`
        //paint(gameObject);
      }
    }
    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.name == "wall")

        {
            touchingWall = false;
            Debug.Log(touchingWall + "TW false");
            //collision.gameObject.SendMessage("in front of the wall")`
            //paint(gameObject);
        }
    }


}