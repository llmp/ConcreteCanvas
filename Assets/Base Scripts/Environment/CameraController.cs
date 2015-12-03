using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	float fixedX;
	float fixedY;

	// Use this for initialization
	void Start(){
		setFixXOnPlayer();
		setFixYOnPlayer();
	}
	
	// Update is called once per frame
	void Update(){
		youShallNotPass(GameObject.Find("player").transform.position.x, GameObject.Find("player").transform.position.y);
		fixCamera(fixedX, fixedY);
		gameObject.GetComponent<BoundariesChecker>().checkBorders(transform.position);

	}
	
	private void fixCamera(float xPosition, float yPosition){
		gameObject.transform.position = new Vector3(xPosition,
		                                            yPosition,
		                                            gameObject.transform.position.z);
	}

	private void setFixXOnPlayer(){
		fixedX = GameObject.Find("player").transform.position.x;
	}

	private void setFixYOnPlayer(){
		fixedY = GameObject.Find("player").transform.position.y;
	}

	private void youShallNotPass(float newX, float newY){
		bool isBot = gameObject.GetComponent<BoundariesChecker>().isTouchingBottom;
		bool isTop = gameObject.GetComponent<BoundariesChecker>().isTouchingTop;
		bool isRight = gameObject.GetComponent<BoundariesChecker>().isTouchingRight;
		bool isLeft = gameObject.GetComponent<BoundariesChecker>().isTouchingLeft;

		Debug.Log ( "Bot: " + isBot + ", Top: " + isTop + ", Right: " + isRight + ", Left: " + isLeft);

		if (!isBot && !isTop && !isRight && !isLeft){
			setFixXOnPlayer();
			setFixYOnPlayer();
		}

		else{
			Vector3 current = transform.position;
			if (isLeft && isBot){
				if (newY > current.y && newX > current.x){
					fixedY = newY;
					fixedX = newX;
				}
				else if (newY < current.y && newX > current.x){
					fixedY = current.y;
					fixedX = newX;
				}
				else if (newY > current.y && newX < current.x){
					fixedY = newY;
					fixedX = current.x;
				}
			}
			else if (isLeft && isTop){
				if (newY < current.y && newX > current.x){
					fixedY = newY;
					fixedX = newX;
				}
				else if (newY > current.y && newX > current.x){
					fixedY = current.y;
					fixedX = newX;
				}
				else if (newY < current.y && newX < current.x){
					fixedY = newY;
					fixedX = current.x;
				}
			}
			else if (isRight && isBot){
				if (newY > current.y && newX < current.x){
					fixedY = newY;
					fixedX = newX;
				}
				else if (newY < current.y && newX < current.x){
					fixedY = current.y;
					fixedX = newX;
				}
				else if (newY > current.y && newX > current.x){
					fixedY = newY;
					fixedX = current.x;
				}
			}
			else if (isRight && isTop){
				if (newY < current.y && newX < current.x){
					fixedY = newY;
					fixedX = newX;
				}
				else if (newY > current.y && newX < current.x){
					fixedY = current.y;
					fixedX = newX;
				}
				else if (newY < current.y && newX > current.x){
					fixedY = newY;
					fixedX = current.x;
				}
			}
			else if (isBot && newY < current.y){
				fixedY = current.y;
				fixedX = newX;
			}
			else if (isBot && newY > current.y){
				fixedY = newY;
				fixedX = newX;
			}
			else if (isTop && newY > current.y){
				fixedY = current.y;
				fixedX = newX;
			}
			else if (isTop && newY < current.y){
				fixedY = newY;
				fixedX = newX;
			}
			else if (isLeft && newX < current.x){
				fixedX = current.x;
				fixedY = newY;
			}
			else if (isLeft && newX > current.x){
				fixedX = newX;
				fixedY = newY;
			}
			else if (isRight && newX > current.x){
				fixedX = current.x;
				fixedY = newY;
			}
			else if (isRight && newX < current.x){
				fixedX = newX;
				fixedY = newY;
			}
		}
		Debug.Log ( "X: " + fixedX + ",Y:" + fixedY);

	}
}