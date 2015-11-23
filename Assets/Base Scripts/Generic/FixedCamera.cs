using UnityEngine;
using System.Collections;

public class FixedCamera : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(GameObject.Find("player").transform.position.x,
		                                            GameObject.Find("player").transform.position.y,
		                                            gameObject.transform.position.z);
	}

	void checkBorders(){

	}
}
