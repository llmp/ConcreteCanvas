using UnityEngine;
using System.Collections;

public class BoundariesChecker : MonoBehaviour {

	public bool isTouchingTop = false;
	public bool isTouchingBottom = false;
	public bool isTouchingLeft = false;
	public bool isTouchingRight = false;

	private struct Coordinates {
		public float xMax;
		public float xMin;
		public float yMax;
		public float yMin;
	}

	// HIC SUNT DRACONES
	public short getPossibleMoves(Vector3 checkVector){
		short movePossibilities = 4;
		Coordinates coordinates = getRelativeCoordinates("background");

		if (checkVector.x <= coordinates.xMin + 0.3f) {
			movePossibilities --;
		}

		else if(checkVector.x >= coordinates.xMax - 0.3f){
			movePossibilities --;
		}

		if(checkVector.y <= coordinates.yMin + 0.3f){
			movePossibilities --;
		}

		else if(checkVector.y >= coordinates.yMax - 0.3f){
			movePossibilities --;
		}

		return movePossibilities;
	}

	public void checkBorders(Vector3 checkVector, float proximityXFactor, float proximityYFactor){
		unflagAllTouchChecks();
		Coordinates coordinates = getRelativeCoordinates("background");

		if (checkVector.x <= coordinates.xMin + proximityXFactor) {
			isTouchingLeft = true;
		}
		
		else if(checkVector.x >= coordinates.xMax - proximityXFactor){
			isTouchingRight = true;
		}
		
		if(checkVector.y <= coordinates.yMin + proximityYFactor){
			isTouchingBottom = true;
		}
		
		else if(checkVector.y >= coordinates.yMax - proximityYFactor){
			isTouchingTop = true;
		}
	}

	private Coordinates getRelativeCoordinates(string reference){
		Camera mainCamera = Camera.main;
		Coordinates coordinates;

//		if (!precise){
			float xDist = mainCamera.aspect * mainCamera.orthographicSize;
			coordinates.xMax = GameObject.Find(reference).transform.position.x + xDist;
			coordinates.xMin = GameObject.Find(reference).transform.position.x - xDist;

			float yDist = mainCamera.orthographicSize;
			coordinates.yMax = GameObject.Find(reference).transform.position.y + yDist;
			coordinates.yMin = GameObject.Find(reference).transform.position.y - yDist;
//		}
//		else {
//			float xDist = mainCamera.aspect * mainCamera.orthographicSize;
//			coordinates.xMax = GameObject.Find(reference).transform.position.x;
//			coordinates.xMin = GameObject.Find(reference).transform.position.x;
//			
//			float yDist = mainCamera.orthographicSize;
//			coordinates.yMax = GameObject.Find(reference).transform.position.y;
//			coordinates.yMin = GameObject.Find(reference).transform.position.y;
//		}

		return coordinates;
	}

	public void unflagAllTouchChecks(){
		isTouchingTop = false;
		isTouchingRight = false;
		isTouchingBottom = false;
		isTouchingLeft = false;
	}

}