using UnityEngine;
using System.Collections.Generic;

public class PlayerBulletPool : MonoBehaviour {

	//public int poolSize = 5;
	public GameObject[] instances;
	//public bool prePopulate;
    List<GameObject> pool_colorA = new List<GameObject>();
    List<GameObject> pool_colorB = new List<GameObject>();
    List<GameObject> pool_colorC = new List<GameObject>();
    
 
	//public List<GameObject> poolInstances = new List<GameObject> ();
 
	void Start () {
		//if (prePopulate)
			//PrePolpulate ();
	}
 
	/*public void PrePolpulate(){
		for (var i = 0; i < poolSize; i++){
			var clone = CreateInstance("blue_red");
			clone.SetActive(false);
		}
	}*/
 
	public GameObject NextObject(WeaponColor color){
        var pool = pool_colorA;
        if (color == WeaponColor.colorA) {
            pool = pool_colorA;
        }else if (color == WeaponColor.colorC){
            pool = pool_colorB;
        }else if (color == WeaponColor.colorB) {
            pool = pool_colorC;
        }
		foreach (var instance in pool) {
			if(instance.activeSelf != true){
                return instance;
			}
		}
		return CreateInstance(color);
	}
 
	private GameObject CreateInstance(WeaponColor color){
        int index = 0;
        if (color == WeaponColor.colorA) {
            index = 0;
        }else if (color == WeaponColor.colorC){
            index = 1;
        }else if (color == WeaponColor.colorB) {
            index = 2;
        }
		var clone =  Instantiate (instances [index]) as GameObject;
        clone.SetActive(false);
        clone.transform.position = Vector2.zero;
		
        if (index == 0) {
            pool_colorA.Add(clone);
        }else if (index == 1){
            pool_colorB.Add(clone);
        }else if (index == 2){
            pool_colorC.Add(clone);
        }
		return clone;
	}
}
