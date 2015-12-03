using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {
	public float speed;
	//string c;
	
	// Use this for initialization
	void Start () {
		//c = GetComponent<SpriteRenderer>().sprite.name;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(transform.position.x - speed, transform.position.y);
	}
	
	void OnBecameInvisible() {
		gameObject.SetActive(false);
		transform.position = Vector2.zero;
	}
}
