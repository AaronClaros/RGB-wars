using UnityEngine;
using System.Collections.Generic;

public class PlayerBulletPool : MonoBehaviour {

	//public int poolSize = 5;
	public GameObject[] instances;
	//public bool prePopulate;
    List<GameObject> pool_br = new List<GameObject>();
    List<GameObject> pool_gr = new List<GameObject>();
    List<GameObject> pool_gb = new List<GameObject>();
    
 
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
 
	public GameObject NextObject(header color){
        var pool = pool_br;
        if (color == header.blue_red) {
            pool = pool_br;
        }else if (color == header.green_red){
            pool = pool_gr;
        }else if (color == header.green_blue) {
            pool = pool_gb;
        }
		foreach (var instance in pool) {
			if(instance.activeSelf != true){
                return instance;
			}
		}
		return CreateInstance(color);
	}
 
	private GameObject CreateInstance(header color){
        int index = 0;
        if (color == header.blue_red) {
            index = 0;
        }else if (color == header.green_red){
            index = 1;
        }else if (color == header.green_blue) {
            index = 2;
        }
		var clone =  Instantiate (instances [index]) as GameObject;
        clone.SetActive(false);
        clone.transform.position = Vector2.zero;
		
        if (index == 0) {
            pool_br.Add(clone);
        }else if (index == 1){
            pool_gr.Add(clone);
        }else if (index == 2){
            pool_gb.Add(clone);
        }
		return clone;
	}
}
