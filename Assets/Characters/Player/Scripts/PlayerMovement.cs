using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
    
    //Faces
    private bool standingUp = false;
    private bool standingDown = false;
    private bool standingLeft = false;
    private bool standingRight = false;

    //Interactions
    private bool touchingBBuilding = false;
    private bool touchingSignO = false;
    private bool touchingStationO = false;
    private bool touchingSBuilding = false;
    private bool touchingDonut = false;
    private bool touchingSkate = false;
    private bool touchingSpray = false;
    private bool touchingBucket = false;

    //audio
    private AudioSource aud;
    [SerializeField] private AudioClip sprayClip;
    

    //Movement
    private bool isWalking = false;
	[SerializeField]
	private float moveSpeed = 0.02f;
	private short walkingDirection;

	//BorderCheck
	private bool canWalkUp = true;
	private bool canWalkBottom = true;
	private bool canWalkRight = true;
	private bool canWalkLeft = true;

    //others
    private int sprayLoad = 2; 
	private Animator animator;
    //private []int bag;
    private GameObject aux;
    private float time = 0f;

	void Start () {
		animator = gameObject.GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
    }

	void Update () {
		getGridPosition();
        if (animator.GetBool("isSpraying") )
        {
            if(time <= 5)
            {
                time += Time.deltaTime;
                if (!aud.isPlaying)
                {
                    aud.Play();
                }
                
            }
            else
            {
                aud.Stop();
                paint();
                animator.SetBool("isSpraying", false);
                time = 0;
            }
            
        }
        else
        {
            getKeyDown();
            getFacingDirection();
            getKeyUp();
            getInteractionKey();

        }

    }

	private void getKeyDown(){
		if (Input.GetKey (KeyCode.D) && canWalkRight) {
			if (!isWalking){
				activateAnimation("isWalkingRight");
				isWalking = true;
				walkingDirection = 2;
				unflagAllBoolColliders();
			}
			transform.position += new Vector3(moveSpeed,0,0);
		} 
		else if (Input.GetKey (KeyCode.A) && canWalkLeft) {
			if (!isWalking){
				activateAnimation("isWalkingLeft");
				isWalking = true;
				walkingDirection = 4;
				unflagAllBoolColliders();
			}
			transform.position += new Vector3(-(moveSpeed),0,0);
		} 
		else if (Input.GetKey(KeyCode.W) && canWalkUp) {
			if (!isWalking){
				activateAnimation("isWalkingUp");
				isWalking = true;
				walkingDirection = 1;
				unflagAllBoolColliders();
			}
			transform.position += new Vector3(0,moveSpeed,0);
			
		} 
		else if (Input.GetKey(KeyCode.S) && canWalkBottom){
			if (!isWalking){
				activateAnimation("isWalkingBot");
				isWalking = true;
				walkingDirection = 3;
				unflagAllBoolColliders();
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
			if (touchingBBuilding && standingUp)
			{
				interact("bBuildingO");
			}
            if (touchingSignO && standingUp)
            {
                interact("signO");
            }
            if (touchingStationO && standingUp)
            {
                interact("stationO");
            }
            if (touchingSBuilding && standingUp)
            {
                interact("sBuildingO");
            }
            else if (touchingDonut)
			{
				interact("donut(Clone)");
			}
            else if (touchingSkate)
            {
                interact("skateboard(Clone)");
            }
            else if (touchingSpray)
            {
                interact("spray(Clone)");
            }
            else if (touchingBucket)
            {
                interact("bucket(Clone)");
            }
        }
	}

	private void activateAnimation(string animationStr){
		animator.SetBool("isWalkingUp",false);
		animator.SetBool("isWalkingRight",false);
		animator.SetBool("isWalkingLeft",false);
		animator.SetBool("isWalkingBot",false);
		animator.SetBool("isStanding",false);
        animator.SetBool("isSpraying", false);
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
    void startPainting()
    {   
        Debug.Log(aux.name);
        if (sprayLoad > 0 && !animator.GetBool("isSpraying") )
        {
            activateAnimation("isSpraying");
            sprayLoad--;
        }
        else if(sprayLoad == 0)
        {
            Debug.Log("Out of ink");
        }

    }

    void dropBucket()
    {

        //GameObject.Find("bucket").GetComponent<SpriteRenderer>().sprite = ;
        //GameObject floorPaint = new GameObject();
        //floorPaint.transform.position = gameObject.transform.position;
        //create an ink object here
        //floorPaint.AddComponent<SpriteRenderer>();
    }

	private void interact(string str){
		if (str == "bBuildingO" || str== "sBuildingO" || str == "signO"|| str =="stationO" )
		{
			startPainting();
		} 
		else if(str == "donut(Clone)")
		{
            
            GameObject.Destroy(GameObject.Find("donut(Clone)"));
            touchingDonut = false;
            GameObject.Find("cop").GetComponent<CopMovement>().eatDonut();

        }
        else if(str == "skateboard(Clone)")
        {
            gameObject.GetComponent<PlayerMovement>().moveSpeed *= 3;
            GameObject.Destroy(GameObject.Find("skateboard(Clone)"));
            touchingSkate = false;

            GameObject.Find("controller").GetComponent<SoundController>().changeAudio(); 
        }
        else if(str == "bucket")
        {
            dropBucket();
            touchingBucket = false;
        }
        else if (str == "spray(Clone)")
        {
            sprayLoad = 2;
            Debug.Log(sprayLoad + "loads");
            GameObject.Destroy(GameObject.Find("spray(Clone)"));
            touchingSpray = false;
        }

        else
        {
            Debug.Log("Error");
        }
	}

    private void paint()
    {
        GameObject.Find("controller").GetComponent<ObjectiveControl>().changeO(aux);
        GameObject.Find("controller").GetComponent<ObjectiveControl>().generatePowerUp();
    }

    void pickUpItem(){
//       GameObject obj= GameObject.Find("cop");
    }

	void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.tag == "obstacle" || collider.tag == "npc"){
			setBoolCollider(false);
		}
        //to not get other colliders
        if (collider.name == "bBuildingO" || collider.name == "stationO" || collider.name == "signO" || collider.name == "sBuildingO")
        {
            aux = collider.gameObject;
        }

        if (collider.name == "bBuildingO")
        {
            touchingBBuilding = true;
        }

        if (collider.name == "signO")
        {
            touchingSignO = true;
        }
        if (collider.name == "stationO")
        {
            touchingStationO = true;
        }
        if (collider.name == "sBuildingO")
        {
            touchingSBuilding = true;
        }
        else if (collider.name == "donut(Clone)")
		{
			touchingDonut= true;
		}
		else if (collider.name == "skateboard(Clone)")
		{
			touchingSkate = true;
		}
		else if (collider.name == "spray(Clone)")
		{ 
			touchingSpray = true;
		}
		else if (collider.name == "bucket")
		{
			touchingBucket = true;
		}
    }

	void OnTriggerStay2D(Collider2D collider){
		if (collider.tag == "obstacle"){
			setBoolCollider(false);
		}
	}

    void OnTriggerExit2D(Collider2D collider){

        if (collider.name == "bBuildingO")
        {
            touchingBBuilding = false;
        }
        if (collider.name == "signO")
        {
            touchingSignO = false;
        }
        if (collider.name == "stationO")
        {
            touchingStationO = false;
        }
        if (collider.name == "sBuildingO")
        {
            touchingSBuilding = false;
        }

        else if (collider.name == "donut(Clone)")
        {
            touchingDonut = false;
        }
		else if (collider.name == "skateboard(Clone)")
        {
            touchingSkate = false;
        }
		else if (collider.name == "spray(Clone)")
        {
            touchingSpray = false;
        }
		else if (collider.name == "bucket(Clone)")
        {
            touchingBucket = false;
        }
    }

	private void setBoolCollider(bool value){
		if (walkingDirection == 1){
			this.canWalkUp = value;
		}
		else if (walkingDirection == 2){
			this.canWalkRight = value;
		}
		else if (walkingDirection == 3){
			this.canWalkBottom = value;
		}
		else if (walkingDirection == 4){
			this.canWalkLeft = value;
		}
	}

	private void unflagAllBoolColliders(){
		this.canWalkUp = true;
		this.canWalkRight = true;
		this.canWalkBottom = true;
		this.canWalkLeft = true;
	}

	private short[] getGridPosition(){
		short[] playerGridPosition =  new short[2];
		Vector3 playerPosition =this.transform.position;

		Debug.Log("Player Position:" + (playerPosition.x * 100) + "," + (playerPosition.y * 100));

		return playerGridPosition;
	}

}