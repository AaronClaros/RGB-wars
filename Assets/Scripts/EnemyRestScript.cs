using UnityEngine;
using System.Collections;

public class EnemyRestScript : MonoBehaviour {
	public float speed = 1;
	float rspeed;
	int rotateSide;
    Vector3 rDirection;

	//public SideColor color;
    void Awake() {
        rDirection = new Vector3(Random.Range(-0.5f, 0.2f), Random.Range(-0.9f, 0.9f), 0f);
    }

	// Use this for initialization
	void Start () {
		rspeed = Random.Range (speed/5, speed/2);
		rotateSide = Random.Range (-1, 1);
		if (rotateSide >= 0) {
			rspeed=rspeed*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 2*rspeed), Space.Self);
		transform.position += rDirection * (speed * Time.deltaTime);
	}

    public void SetDirection(Vector3 direction) {
        rDirection = direction;
    }
}
