using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoGUIScript : MonoBehaviour {
    public Sprite[] ammoSprites;
    public Sprite emptyAmmo;
    Image img;

    public static AmmoGUIScript instance = null;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this);
    }

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
    public void IncreaseAmmoCountGUI(int count){
        int actAmmo = PlayerController.actualAmmo;
        int ammoC = PlayerController.ammoCount;
        if (count < ammoSprites.Length-1 & (actAmmo+count) < ammoC){
            Debug.Log("increase ammo by "+count);
            img.sprite = ammoSprites[actAmmo + count];
            PlayerController.actualAmmo++;
        }
        else if (count >= ammoSprites.Length | (actAmmo+count) >= ammoC ){
            Debug.Log("Filled ammo by " + PlayerController.actualAmmo);
            img.sprite = ammoSprites[ammoC];
            PlayerController.actualAmmo = ammoC;
        }
    }

    public void SetAmmoGUICount(int value) {
        if (value < ammoSprites.Length) {
            img.sprite = ammoSprites[value];
        }
    }
}
