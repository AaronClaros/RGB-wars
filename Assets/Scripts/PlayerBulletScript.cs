using UnityEngine;
using System.Collections;

public class PlayerBulletScript : MonoBehaviour {
    public float speed;
    public SideColor color;
    [HideInInspector]
    //public SideColor colorA;
    
    //public SideColor colorB;
    //string c;
    
	// Use this for initialization
	void Start () {
        /*
        c = GetComponent<SpriteRenderer>().sprite.name;
        
        if (c == "blue_red") {
            color = WeaponColor.colorA;
            colorA = SideColor.blue;
            colorB = SideColor.red;
        }
        else if (c == "green_blue")
        {
            color = WeaponColor.colorB;
            colorA = SideColor.green;
            colorB = SideColor.blue;
        }
        else if (c == "green_red")
        {
            color = WeaponColor.colorC;
            colorA = SideColor.green;
            colorB = SideColor.red;
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
	}

    void OnBecameInvisible() {
        gameObject.SetActive(false);
        transform.position = Vector2.zero;
    }
}
