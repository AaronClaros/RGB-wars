using UnityEngine;
using System.Collections;

public class EnemyRestScript : MonoBehaviour {
	public float speed = 1;
	float rspeed;
	int rotateSide;
    Vector3 rDirection;
	public SideColor color;
	// Use this for initialization
	void Start () {
		rspeed = Random.Range (speed, speed*3);
		rotateSide = Random.Range (-1, 1);
		if (rotateSide >= 0) {
			rspeed=rspeed*-1;
		}
        rDirection = new Vector3(Random.Range(0f, 1f), Random.Range(-1f, 1f), 0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 2*rspeed), Space.Self);
		transform.position -= rDirection * speed;
	}


}
