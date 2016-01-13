using UnityEngine;
using System.Collections;

public enum WeaponColor { 
    colorA = -1, 
    colorB = 0, 
    colorC = 1 
};

public enum SideColor { 
    red,
    green,
    blue
};
public class PlayerController : MonoBehaviour {
    //[SerializeField]
    //ForceMode2D forceMode;
    //Rigidbody2D rb2d;
	public enum ControlType{
		keyboard_only,
		keyboard_mouse
	};
	public ControlType controlType;
	public float moveSpeed = 0.3f;
    public float rotateSpeed = 0.1f;
    public static int actualAmmo = 10;
    public static int ammoCount = 10;
    public float reloadTime = 1;
    public float fireRate = 10f;
    public string bulletColor = "blue_red";
    public bool rotating = false;
    public bool shooting = false;

    float lastfired;
    float hAxis;
    float vAxis;
    
    PlayerBulletPool pool;
    Quaternion startingRotation;
    Transform shields;
    bool rotating_flag = true;
    bool trigger1_pressed = false;
    bool trigger2_pressed = false;
    EnergyBarScript energyBar;
    
    public WeaponColor head;
    private AmmoGUIScript ammoGUI;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        shields = transform.GetChild(0);
        startingRotation = shields.transform.rotation;
		pool = GetComponent<PlayerBulletPool>();
        ammoGUI = FindObjectOfType(typeof (AmmoGUIScript)) as AmmoGUIScript;
        energyBar = FindObjectOfType(typeof(EnergyBarScript)) as EnergyBarScript;
        actualAmmo = ammoCount;
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

        if ((vAxis != 0) & !Input.GetButton("Fire1") || (hAxis != 0) & !Input.GetButton("Fire1"))
        {
            
            energyBar.IncreaseEnergy(0.003f);
        }
        

        //Turning Player
        if ((Input.GetAxis("Left Trigger") <= -0.2 & !Input.GetButton("Fire1")) & !trigger1_pressed)
        {
            Debug.Log(Input.GetAxis("Left Trigger"));
            trigger1_pressed = true;
            if (!rotating)
            {
                Debug.Log("left rotation");
                StopAllCoroutines();
                StartCoroutine(rotateTriangle(120));
                energyBar.IncreaseEnergy(0.2f);
                switch (head)
                {
                    case WeaponColor.colorA:
                        head = WeaponColor.colorC;
                        break;
                    case WeaponColor.colorB:
                        head = WeaponColor.colorA;
                        break;
                    case WeaponColor.colorC:
                        head = WeaponColor.colorB;
                        break;
                    default:
                        break;
                }
            }

        }
        else if (Input.GetAxis("Left Trigger") > -0.1){
            trigger1_pressed = false;
        }



        if ((Input.GetAxis("Right Trigger") <= -0.2 & !Input.GetButton("Fire1")) & !trigger2_pressed)
        {
            Debug.Log(Input.GetAxis("Right Trigger"));
            trigger2_pressed = true;
            if (!rotating)
            {
                Debug.Log("rigth rotation");
                StopAllCoroutines();
                StartCoroutine(rotateTriangle(-120));
                energyBar.IncreaseEnergy(0.2f);
                switch (head)
                {
                    case WeaponColor.colorA:
                        head = WeaponColor.colorB;
                        break;
                    case WeaponColor.colorB:
                        head = WeaponColor.colorC;
                        break;
                    case WeaponColor.colorC:
                        head = WeaponColor.colorA;
                        break;
                    default:
                        break;
                }
            }

        }
        else if (Input.GetAxis("Right Trigger") > -0.1)
        {
            trigger2_pressed = false;
        }
  

		//Shooting with space
		if (Input.GetButton ("Fire1") && (!Input.GetKey (KeyCode.Z) || !Input.GetKey (KeyCode.C))) {

			if (!shooting && actualAmmo > 0){
				if (Time.time - lastfired > 1 / fireRate) {
					lastfired = Time.time;
					var bullet = pool.NextObject (head);
					shooting = true;
					bullet.transform.position = new Vector2(transform.position.x + 2.5f, transform.position.y);
					bullet.SetActive (true);
                    ammoGUI.ReduceAmmoCountGUI(1);
				}
			}
		} else {
			shooting = false;
		}
        
		//Fast Shoot
		if (Input.GetButtonDown ("Fire1")) {
			if (!shooting && actualAmmo > 0){
				var bullet = pool.NextObject (head);
				shooting = true;
                bullet.transform.position = new Vector2(transform.position.x + 2.5f, transform.position.y);
                bullet.SetActive(true);
                ammoGUI.ReduceAmmoCountGUI(1);
			}
		} else {
			shooting = false;
		}
        
        //Restart Game
        if (Input.GetButtonDown("Submit")) {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (energyBar.GetEnergyValue() == 1) {
            actualAmmo = ammoCount;
            energyBar.SetEnergyValue(0);
            ammoGUI.SetAmmoGUICount(ammoCount);
        }

		// block mode
        /*
		if (Input.GetKey (KeyCode.Space) && Input.GetKeyDown (KeyCode.C)) {
			//if (!rotating)
			//{
			Debug.Log ("Left Block on");
			StartCoroutine (rotateTriangle (-60));
			//}
		} else if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.C)) {
			rotating = true;
			shooting = true;
		}else if (Input.GetKey (KeyCode.Space) && Input.GetKeyUp (KeyCode.C)){
			//if (!rotating){
				rotating = false;
				Debug.Log("Left Block off");
				StartCoroutine(rotateTriangle(60));
			//}
		}

        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.Z))
        {
            //if (!rotating)
            //{
            Debug.Log("Left Block on");
            StartCoroutine(rotateTriangle(60));
            //}
        }
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.Z))
        {
            rotating = true;
            shooting = true;
        }
        else if (Input.GetKey(KeyCode.Space) && Input.GetKeyUp(KeyCode.Z))
        {
            //if (!rotating){
            rotating = false;
            Debug.Log("Left Block off");
            StartCoroutine(rotateTriangle(-60));
            //}
        }
        */
    }

    IEnumerator rotateTriangle(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, 0, rotationAmount) * startingRotation;
        rotating = true;
        while (shields.transform.rotation != finalRotation)
        {
            shields.transform.rotation = Quaternion.Lerp(shields.transform.rotation, finalRotation, Time.deltaTime * rotateSpeed);
            yield return 0;
        }
        rotating = false;
        startingRotation = finalRotation;
        
    }
}
