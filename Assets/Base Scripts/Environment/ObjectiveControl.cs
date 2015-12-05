using UnityEngine;
using System.Collections;

public class ObjectiveControl : MonoBehaviour {
    [SerializeField]    private Sprite signUndone;
    [SerializeField]    private Sprite signDone;
    [SerializeField]    private Sprite stationUndone;
    [SerializeField]    private Sprite stationDone;

    [SerializeField]    private Sprite bBuildingUndone;
    [SerializeField]    private Sprite bBuildingDone;
    [SerializeField]    private Sprite sBuildingUndone;
    [SerializeField]    private Sprite sBuildingDone;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
    public void changeO(GameObject obj)
    {
        if (obj.name == "bBuildingO")
        {
            obj.GetComponent<SpriteRenderer>().sprite = bBuildingDone;
        }else if (obj.name == "signO")
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
        }

    }
}
