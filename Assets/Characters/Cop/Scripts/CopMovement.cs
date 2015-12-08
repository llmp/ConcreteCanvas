using UnityEngine;
using System.Collections;
using System;

public class CopMovement : NPCGenericMovement {

    private bool isEating = false;
    private float time = 0;
    private AudioSource audioSource;
	[SerializeField]
	private float catchableArea = 10f;
	
	void Start(){
		initializeAnimator(gameObject.GetComponent<Animator>());
        audioSource = gameObject.GetComponent<AudioSource>();
    }

	void Update(){
		GameObject player = GameObject.Find("player");
//		catchPlayer(player.transform.position);
		this.lookFor(player);
        if (isEating )
        {
            eatDonut();
        }
        else if(autoMotion)
//        {
//			if (isChasing){
				chasePlayer();
//			}
//			else{
//				applyMotion();
//			}
//        }
    }

	private void lookFor(GameObject objective){
		base.lookFor(objective);
		catchPlayer(objective.transform.position);
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
		float xMax = copPosition.x + catchableArea * 3;
		float xMin = copPosition.x - catchableArea * 3;
		float yMax = copPosition.y + catchableArea * 3;
		float yMin = copPosition.y - catchableArea * 3;
		
		if (objectivePosition.x >= xMin && objectivePosition.x <= xMax && objectivePosition.y >= yMin && objectivePosition.y <= yMax){
			Application.LoadLevel("loseScene");
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
			onCollisionChangeDirection();
	}
}

