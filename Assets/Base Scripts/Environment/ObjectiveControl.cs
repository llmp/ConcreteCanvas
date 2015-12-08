using UnityEngine;
using System.Collections;

public class ObjectiveControl : MonoBehaviour
{
    [SerializeField]
    private Sprite signUndone;
    [SerializeField]
    private Sprite signDone;
    private bool signPainted = false;

    [SerializeField]
    private Sprite stationUndone;
    [SerializeField]
    private Sprite stationDone;
    private bool stationPainted = false;

    [SerializeField]
    private Sprite bBuildingUndone;
    [SerializeField]
    private Sprite bBuildingDone;
    private bool bBuildingPainted = false;

    [SerializeField]
    private Sprite sBuildingUndone;
    [SerializeField]
    private Sprite sBuildingDone;
    private bool sBuildingPainted = false;
    [SerializeField]
    private Sprite sBuildingTopUndone;
    [SerializeField]
    private Sprite sBuildingTopDone;

    [SerializeField]
    private Sprite mapVictory;
    private float victoryTime = 0f;
    private bool victoryScreen=false;
    private int objectivesDone = 0;
    private int currentPowerUp = 0;

    
    //
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (victoryScreen)
        {
            victoryTime += Time.deltaTime;
            if (victoryTime >= 3f)
            {
                victoryScreen = false;
                Application.LoadLevel("StartScene");
            }
        }
        else
        {

            checkVictory();

        }
    }
    public void changeO(GameObject obj)
    {
        if (obj.name == "bBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = bBuildingDone;
            bBuildingPainted = true;
        }
        else if (obj.name == "signO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = signDone;
            signPainted = true;
        }
        else if (obj.name == "stationO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = stationDone;
            stationPainted = true;
        }
        else if (obj.name == "sBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = sBuildingDone;
            GameObject.Find("sBuildingTopO").GetComponent<SpriteRenderer>().sprite = sBuildingTopDone;
            sBuildingPainted = true;
        }
        objectivesDone++;

    }

    public void changeOBack(GameObject obj)
    {
        if (obj.name == "bBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = bBuildingUndone;
            bBuildingPainted = false;
        }
        else if (obj.name == "signO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = signUndone;
            signPainted = false;
        }
        else if (obj.name == "stationO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = stationUndone;
            stationPainted = false;
        }
        else if (obj.name == "sBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = sBuildingUndone;
            GameObject.Find("sBuildingTopO").GetComponent<SpriteRenderer>().sprite = sBuildingTopUndone;
            sBuildingPainted = false;
        }
        objectivesDone--;

    }
    public void checkVictory()
    {
        if (objectivesDone >= 4)
        {
            paintMap();
            victoryScreen = true;
            objectivesDone = 0;
        }

    }

    public void generatePowerUp()
    {
        GameObject controller = GameObject.Find("controller");
        if ((currentPowerUp % 3) == 0)
        {
            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[1], new Vector3(12.3f, -5.5f, 0));
        }
        else if ((currentPowerUp % 3) == 2)
        {

            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[3], new Vector3(18f, -11f, 0));
        }

         if (((currentPowerUp % 3) == 1)|| (currentPowerUp %2)== 1)
        {

            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[2], new Vector3(1f, -11f, 0));

        }

        currentPowerUp++;

    }
    void paintMap()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().setVictoryCamera();
        GameObject.Destroy(GameObject.Find("map objects"));
        GameObject.Find("bBuildingO").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("signO").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("sBuildingO").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("sBuildingTopO").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("stationO").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("sprayCanUI1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("sprayCanUI2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("painter").GetComponent<PainterMovement>().working = false;

        //GameObject.Find("stationO").GetComponent<SpriteRenderer>().enabled = false;



        //GameObject.Destroy(GameObject.Find("objectives"));
        //GameObject.Destroy(GameObject.Find("UI"));
        GameObject.Find("background").GetComponent<SpriteRenderer>().sprite = mapVictory;
        GameObject.Find("controller").GetComponent<PrefabFactory>().instantiatePrefab(GameObject.Find("controller").GetComponent<PrefabFactory>().prefabArray[4], new Vector3(11.55f, -2f, 0));
        GameObject.Find("painter").GetComponent<NPCGenericMovement>().setMovement(false);
        GameObject.Find("cop").GetComponent<NPCGenericMovement>().setMovement(false);
    }

    public bool verifyPainted(GameObject obj)
    {
        if (obj.name == "signO")
        {
            return signPainted;
        }else if (obj.name == "stationO")
        {
            return stationPainted;
        }
        else if (obj.name == "sBuildingO")
        {
            return sBuildingPainted;
        }
        else if (obj.name == "bBuildingO")
        {
            return bBuildingPainted;
        }
        else
        {
            return false;
        } 

    }

}
