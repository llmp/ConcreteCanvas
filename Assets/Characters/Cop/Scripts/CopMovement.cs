using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {

    private bool isEating = false;
    private float time = 0;
    private AudioSource aSource;
	
	void Start(){
		initializeAnimator(gameObject.GetComponent<Animator>());
        aSource = gameObject.GetComponent<AudioSource>();
    }

	void Update(){
        
        
        if (isEating)
        {
            eatDonut();
        }
        else
        {
            lookFor(GameObject.Find("player").transform.position);
            applyMotion();
        }
    }

	public void eatDonut(){
        isEating = true;
        Debug.Log("The cop is eating a delicious donut");
        time += Time.deltaTime;

        if (!aSource.isPlaying)
        {
            aSource.Play();
        }

        if (time > 5)
        {
            //          if()
            
            time = 0;
            isEating = false;
        }
        
        
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name.CompareTo("player") == 1){
		}
		else {
			onCollisionChangeDirection();
		}
	}
}