using UnityEngine;
using System.Collections;

public class PlayerBulletScript : MonoBehaviour {
    public float speed;
    public header color;
    [HideInInspector]
    public colorGame colorA;
    [HideInInspector]
    public colorGame colorB;
    string c;
    
	// Use this for initialization
	void Start () {
        c = GetComponent<SpriteRenderer>().sprite.name;
        if (c == "blue_red") {
            color = header.blue_red;
            colorA = colorGame.blue;
            colorB = colorGame.red;
        }
        else if (c == "green_blue")
        {
            color = header.green_blue;
            colorA = colorGame.green;
            colorB = colorGame.blue;
        }
        else if (c == "green_red")
        {
            color = header.green_red;
            colorA = colorGame.green;
            colorB = colorGame.red;
        }
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
