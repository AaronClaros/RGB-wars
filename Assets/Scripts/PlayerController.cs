using UnityEngine;
using System.Collections;

public enum header { 
    blue_red = -1, 
    green_blue = 0, 
    green_red = 1 
};
public class PlayerController : MonoBehaviour {
    //[SerializeField]
    //ForceMode2D forceMode;
    //Rigidbody2D rb2d;
    public float moveSpeed = 0.3f;
    public float rotateSpeed = 0.1f;
    public float fireRate = 10f;
    public string bulletColor = "blue_red";
    float lastfired;
    float hAxis;
    float vAxis;
    public bool rotating = false;
    public bool shooting = false;
    BulletPool pool;
    Quaternion startingRotation;

    
    public header head;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        startingRotation = this.transform.rotation;
        pool = GetComponent<BulletPool>();
	}
	
	// Update is called once per frame
	void Update () {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        //Moving Player
        if (hAxis != 0)
        {
            transform.position = new Vector2(transform.position.x + hAxis * moveSpeed, transform.position.y);
        }
        if (vAxis != 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + vAxis * moveSpeed);
        }
        //Turning Player
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!rotating)
            {
                StartCoroutine(rotateTriangle(120));
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
           
            if (!rotating)
            {
                StartCoroutine(rotateTriangle(-120));
            }
            
        }

        //Shooting
        if (Input.GetButton("Jump")) 
        {
           
            if (Time.time - lastfired > 1 / fireRate)
            {
                lastfired = Time.time;
                var bullet = pool.NextObject(head);
                shooting = true;
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
        else
        {
            shooting = false;
        }

		if (Input.GetButtonDown ("Jump"))
		{
			var bullet = pool.NextObject(head);
			shooting = true;
			bullet.transform.position = transform.position;
			bullet.SetActive(true);
		}
		else
		{
			shooting = false;
		}
        
    }

    IEnumerator rotateTriangle(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, 0, rotationAmount) * startingRotation;
        rotating = true;

        var angle = finalRotation.eulerAngles.z;
        
        if (angle > -5 && angle < 5)
        {
            head = header.blue_red;
        }
        if (angle > 115 && angle < 125)
        {
            head = header.green_red;
        }
        if (angle > 235 && angle < 245)
        {
            head = header.green_blue;
        }

        while(this.transform.rotation != finalRotation)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime*rotateSpeed);
            yield return 0;
        }
        startingRotation = finalRotation;
        rotating = false;

        
    }
}
