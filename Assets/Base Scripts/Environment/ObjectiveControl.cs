using UnityEngine;
using System.Collections;

public class ObjectiveControl : MonoBehaviour
{
    [SerializeField]
    private Sprite signUndone;
    [SerializeField]
    private Sprite signDone;
    [SerializeField]
    private Sprite stationUndone;
    [SerializeField]
    private Sprite stationDone;

    [SerializeField]
    private Sprite bBuildingUndone;
    [SerializeField]
    private Sprite bBuildingDone;


    [SerializeField]
    private Sprite sBuildingUndone;
    [SerializeField]
    private Sprite sBuildingDone;

    [SerializeField]
    private Sprite sBuildingTopUndone;
    [SerializeField]
    private Sprite sBuildingTopDone;

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
        checkVictory();

    }
    public void changeO(GameObject obj)
    {
        if (obj.name == "bBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = bBuildingDone;
        }
        else if (obj.name == "signO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = signDone;
        }
        else if (obj.name == "stationO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = stationDone;
        }
        else if (obj.name == "sBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = sBuildingDone;
            GameObject.Find("sBuildingTopO").GetComponent<SpriteRenderer>().sprite = sBuildingTopDone;
        }
        objectivesDone++;

    }
    public void checkVictory()
    {
        if(objectivesDone >= 4)
        {
            paintMap();
        }

    }

    public void generatePowerUp()
    {
        GameObject controller = GameObject.Find("controller");
        if ((currentPowerUp % 3) == 0)
        {
            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[1], new Vector3(5.5f,3.5f,0));
        } else if (((currentPowerUp % 3) == 1)) {

            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[2], new Vector3(-5f, -1.7f, 0));

        } else if((currentPowerUp % 3) == 2)
        {

            controller.GetComponent<PrefabFactory>().instantiatePrefab(controller.GetComponent<PrefabFactory>().prefabArray[3], new Vector3(11.7f, -2f, 0));
        }
        currentPowerUp++;

    }
    void paintMap()
    {

    }
}
