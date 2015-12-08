using UnityEngine;
using System.Collections;

public class LoseScreen : MonoBehaviour {
    private float time = 0f;
	// Use this for initialization
	void Start () {
        time = 0f;
    }
	
	// Update is called once per frame
	void Update () {
	    if(time > 3f)
        {
            Application.LoadLevel("StartScene");
        }
        else
        {
            time += Time.deltaTime;
        }
	}
}
