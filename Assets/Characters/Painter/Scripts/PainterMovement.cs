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
    
    private GameObject obj1;
    private GameObject obj2;
    private GameObject obj3;
    private GameObject obj4;
    private GameObject objAux;
    public bool working = true;

    private GameObject [] objectives = new GameObject[4];
    private float time = 0f;

    // Use this for initialization
    void Start () {
		initializeAnimator(gameObject.GetComponent<Animator>());
        obj1 = GameObject.Find("sBuildingO");
        obj2 = GameObject.Find("signO");
        obj3 = GameObject.Find("bBuildingO");
        obj4 = GameObject.Find("stationO");
        
        objectives[0] = obj1;
        objectives[1] = obj2;
        objectives[2] = obj3;
        objectives[3] = obj4;
    }



    // Update is called once per frame
    void Update() {
        if (working)
        {
            foreach (GameObject objective in objectives)
            {
                lookFor(objective);
                if (isChasing)
                {
                    if (GameObject.Find("controller").GetComponent<ObjectiveControl>().verifyPainted(objective))
                    {
                        objAux = objective;
                        setMovement(false);
                        break;
                    }
                    isChasing = false;

                }

            }

            if (autoMotion)
            {
                applyMotion();
            }
            else
            {
                if (time <= 3f)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    repaint(objAux);
                    time = 0;
                    setMovement(true);

                }
                //repaint();
                //moveTowardsObject();
            }
        }
        else
        {

        }
    }

   
    public void repaint(GameObject obj)
    {
        GameObject.Find("controller").GetComponent<ObjectiveControl>().changeOBack(obj);
        
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
