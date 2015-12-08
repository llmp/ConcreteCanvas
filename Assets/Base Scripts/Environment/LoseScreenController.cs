using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoseScreenController : MonoBehaviour {

	float loadDelay = 3f;

	// Use this for initialization
	void Start () {
		delayToLoadScreen();
	}

	public void delayToLoadScreen(){
		StartCoroutine(yieldDestroy(loadDelay));
	}
	
	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
	
	IEnumerator yieldDestroy(float timeDelay){
		yield return new WaitForSeconds(timeDelay);
		Application.LoadLevel("StartScene");
	}
}
