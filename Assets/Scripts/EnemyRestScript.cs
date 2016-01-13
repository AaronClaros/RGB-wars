using UnityEngine;
using System.Collections;

public class EnemyRestScript : MonoBehaviour {
	public float speed = 1;
	float rspeed;
	int rotateSide;
	public SideColor color;
	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		rspeed = Random.Range (speed/3, speed);
		rotateSide = Random.Range (-1, 1);
		Debug.Log (rotateSide);
		if (rotateSide >= 0) {
			rspeed=rspeed*-1;
		}
		Debug.Log (rspeed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 2*rspeed), Space.Self);
		transform.position = new Vector2 (transform.position.x - speed/10, transform.position.y);
	}


}
