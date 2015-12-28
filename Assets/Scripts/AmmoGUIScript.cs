using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoGUIScript : MonoBehaviour {
    public Sprite[] ammoSprites;
    Image img;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void ReduceAmmoCountGUI(int count) {
        int actAmmo = PlayerController.actualAmmo;
        if (actAmmo < ammoSprites.Length & actAmmo>0) {
            Debug.Log("reduce ammo");
            img.sprite = ammoSprites[actAmmo - count];
            PlayerController.actualAmmo--;
        }
    }
    public void increaseAmmoCountGUI(int count){
        int actAmmo = PlayerController.actualAmmo;
        int ammoC = PlayerController.ammoCount;
        if (actAmmo < ammoSprites.Length & actAmmo < ammoC){
            Debug.Log("increase ammo");
            img.sprite = ammoSprites[actAmmo + count];
            PlayerController.actualAmmo++;
        }
    }
}
