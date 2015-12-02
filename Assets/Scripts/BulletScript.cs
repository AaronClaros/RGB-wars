using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    public float speed;
    public header color;
    string c;
    
	// Use this for initialization
	void Start () {
        c = GetComponent<SpriteRenderer>().sprite.name;
        if (c == "blue_red") {
            color = header.blue_red;
        }
        else if (c == "green_blue")
        {
            color = header.green_blue;
        }
        else if (c == "green_red")
        {
            color = header.green_red;
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
