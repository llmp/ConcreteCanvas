using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public float startTime;
    private float counterTime;
    private Text timeCounter;
    private PauseScript pauseScene;


	// Use this for initialization
	void Start () {
        counterTime = startTime;
        pauseScene = FindObjectOfType<PauseScript>();
        timeCounter = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if(pauseScene.isPaused)
        {
            return;
        }

        counterTime -= Time.deltaTime;
        if(counterTime <= 0)
        {
            Application.LoadLevel("loseScene");
            //RestTime();
        }
        timeCounter.text = "" + Mathf.Round(counterTime);
	}

    /*
    public void RestTime()
    {
        counterTime = startTime;
    }
    */
}
