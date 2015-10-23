using UnityEngine;
using System.Collections;

public class PrefabFactory : MonoBehaviour {
	
	public GameObject[] prefabArray = new GameObject[4];
	public bool instanceExists = false;

	void Start(){

	}

	public void instantiatePrefab(GameObject prefab, Vector3 position){
		Instantiate(prefab,position,Quaternion.identity);
	}

	public void instantiatePrefabBornToDie(GameObject prefab, Vector3 position, float lifeSpan){
		Object prefabInstance = Instantiate(prefab,position,Quaternion.identity);
		instanceExists = true;
		StartCoroutine(yieldDestroy(prefabInstance,lifeSpan));
	}

	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}

	IEnumerator yieldDestroy(Object prefabInstance, float lifeSpan){
		Destroy(prefabInstance,lifeSpan);
		yield return new WaitForSeconds(lifeSpan);
		instanceExists = false;
	}

}
