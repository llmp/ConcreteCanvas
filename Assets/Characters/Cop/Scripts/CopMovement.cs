using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {

    private bool isEating = false;
    private float time = 0;
    private AudioSource audioSource;
	[SerializeField]
	private float catchableArea = 0.3f;
	
	void Start(){
		initializeAnimator(gameObject.GetComponent<Animator>());
        audioSource = gameObject.GetComponent<AudioSource>();
    }

	void Update(){
		this.lookFor(GameObject.Find("player"));
        if (isEating )
        {
            eatDonut();
        }
        else if(autoMotion)
        {
			if (isChasing){
//				chasePlayer();
			}
			else{
				applyMotion();
			}
        }
    }

	private void lookFor(GameObject objective){
		base.lookFor(objective);
//		catchPlayer(objective.transform.position);
	}

	public void eatDonut(){
        isEating = true;
        time += Time.deltaTime;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (time > 5)
        {
            time = 0;
            isEating = false;
        }   
	}

	private void catchPlayer(Vector3 objectivePosition){
		Vector3 copPosition = this.transform.position;
		float xMax = copPosition.x + catchableArea;
		float xMin = copPosition.x - catchableArea;
		float yMax = copPosition.y + catchableArea;
		float yMin = copPosition.y - catchableArea;
		
		if (objectivePosition.x >= xMin && objectivePosition.x <= xMax && objectivePosition.y >= yMin && objectivePosition.y <= yMax){
			Debug.Log("PLAYER LOSES!!!!!!");
		}
	}

	private void chasePlayer(){
		// Position to refer on csv file
		short[] playerGridPosition = GameObject.Find("player").GetComponent<PlayerMovement>().getGridPosition();
		
		//This will be filled with the information acquired from the csv file.
		//This array will have all the moves for the cop to be at the desired position
		//		short[] nextMoves;
		
		//For now...
		Vector3 playerPosition = GameObject.Find("player").transform.position;
		
		gotoPosition(playerPosition);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name.CompareTo("player") == 1){
		}
		else {
			onCollisionChangeDirection();
		}
	}
}