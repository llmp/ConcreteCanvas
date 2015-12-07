using UnityEngine;
using System.Collections;
using System;

public class PainterMovement : NPCGenericMovement {
    /*

    [SerializeField]
    private short fieldOfViewFront = 3;
    [SerializeField]
    private float fieldOfViewSides = 0.3f;


    private bool movingRight = false;
    private bool movingLeft = false;
    private bool movingUp = false;
    */
    private bool autoMotion = true;
    private GameObject auxObject;

    // Use this for initialization
    void Start () {
		initializeAnimator(gameObject.GetComponent<Animator>());
	}

    

    // Update is called once per frame
    void Update () {

        /*
        search(GameObject.Find("sBuildingO"), GameObject.Find("sBuildingO").transform.position);
        search(GameObject.Find("signO"), GameObject.Find("signO").transform.position);
        search(GameObject.Find("bBuildingO"), GameObject.Find("bBuildingO").transform.position);
        search(GameObject.Find("stationO"), GameObject.Find("stationO").transform.position);
        */
        if (autoMotion)
        {
            applyMotion();
        }
        else
        {
            //repaint();
            //moveTowardsObject();
        }
        
	}


   /* void OnTriggerEnter2D(Collider2D collider)
    {
        //to not get other colliders
        if (collider.name == "bBuildingO" || collider.name == "stationO" || collider.name == "signO" || collider.name == "sBuildingO")
        {
            auxObject = collider.gameObject;
        }
        //moveCharacter(true, false, false);
    }
    */
    //same as lookFor
    /*
    protected void search(GameObject obj, Vector3 objectivePosition)
    {

        float yViewMin;
        float yViewMax;
        float xViewMin;
        float xViewMax;
        Vector3 currentPosition = transform.position;

        if (movingRight)
        {
            yViewMin = currentPosition.y - fieldOfViewSides;
            yViewMax = currentPosition.y + fieldOfViewSides;
            xViewMin = currentPosition.x;
            xViewMax = currentPosition.x + fieldOfViewFront;

            if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax)
            {
                if (GameObject.Find("controller").GetComponent<ObjectiveControl>().verifyPainted(obj))
                {
                    foundObject();
                    repaint(obj);
                }
            }

        }

        else if (movingLeft)
        {
            yViewMin = currentPosition.y - fieldOfViewSides;
            yViewMax = currentPosition.y + fieldOfViewSides;
            xViewMin = currentPosition.x;
            xViewMax = currentPosition.x - fieldOfViewFront;

            if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x <= xViewMin && objectivePosition.x >= xViewMax)
            {
                if (GameObject.Find("controller").GetComponent<ObjectiveControl>().verifyPainted(obj))
                {
                    foundObject();
                    repaint(obj);
                }
            }
        }

        else if (movingUp)
        {
            yViewMin = currentPosition.y;
            yViewMax = currentPosition.y + fieldOfViewFront;
            xViewMin = currentPosition.x - fieldOfViewSides;
            xViewMax = currentPosition.x + fieldOfViewSides;

            if (objectivePosition.y <= yViewMax && objectivePosition.y >= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax)
            {

                if (GameObject.Find("controller").GetComponent<ObjectiveControl>().verifyPainted(obj))
                {
                    foundObject();
                    repaint(obj);
                }
            }
        }

        else
        {
            yViewMin = currentPosition.y;
            yViewMax = currentPosition.y - fieldOfViewFront;
            xViewMin = currentPosition.x - fieldOfViewSides;
            xViewMax = currentPosition.x + fieldOfViewSides;

            if (objectivePosition.y >= yViewMax && objectivePosition.y <= yViewMin && objectivePosition.x >= xViewMin && objectivePosition.x <= xViewMax)
            {

                if (GameObject.Find("controller").GetComponent<ObjectiveControl>().verifyPainted(obj))
                {
                    foundObject();
                    repaint(obj);
                }
            }
        }
    }
    */
    //same as foundSomething
    /*public void foundObject()
    {
        Vector3 instantiateAt = transform.position + new Vector3(0.2f, 0.6f, 0);
        GameObject controller = GameObject.Find("controller");
        if (!controller.GetComponent<PrefabFactory>().instanceExists)
        {
            controller.GetComponent<PrefabFactory>()
                .instantiatePrefabBornToDie(controller.GetComponent<PrefabFactory>().prefabArray[0],
                                            instantiateAt, 1.5f);
        }
    }
    */
    public void repaint(GameObject obj)
    {
        //moveTowardsObject(obj);
    }

    


void OnTriggerEnter2D(Collider2D collider){
//		Debug.Log ("Painter Collision: " + collider.name); 
		if (collider.name.CompareTo("player") == 0){
			GameObject.Find("cop").GetComponent<CopMovement>().foundSomething();
		}
		onCollisionChangeDirection();
	}

	void OnTriggerExit2D(Collider2D collider){
	}
}
