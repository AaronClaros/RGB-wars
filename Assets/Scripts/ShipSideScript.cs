using UnityEngine;
using System.Collections;

public class ShipSideScript : MonoBehaviour {
    
    SpriteRenderer sprite;
    public SideColor sideColor;
    
	float actualXScale;
    AmmoGUIScript ammoGUI;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        ammoGUI = FindObjectOfType(typeof(AmmoGUIScript)) as AmmoGUIScript;
	}

    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("impact");
        if (other.gameObject.tag == "Enemy Bullet") {
            var enemyBullet = other.gameObject.GetComponent<EnemyBulletScript>();
            
            if (enemyBullet.color == sideColor) {
                Debug.Log("Parry Bullet");
                enemyBullet.gameObject.SetActive(false);
                ammoGUI.increaseAmmoCountGUI(1);
            } else {
                DamageSide(20f);
                Debug.Log("Side Damaged");
                enemyBullet.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "Enemy Rest"){
			var enemyRest = other.gameObject.GetComponent<EnemyRestScript>();
			Debug.Log("healed");
			if (enemyRest.color == sideColor) {
				Debug.Log("healed");
				HealtSide(30f);
				enemyRest.gameObject.SetActive(false);
			} else {
				/*
				Debug.Log("Side Damaged");
				enemyRest.gameObject.SetActive(false);*/
			}
        }
        else if (other.gameObject.tag == "Enemy Ship") {
            Debug.Log(other.gameObject.name + " collisioned with " + name);
            var enemyShip = other.gameObject.GetComponent<RedEnemyController>();
            if (enemyShip.shipColor != sideColor)
            {
                DamageSide(20f);
            }
            enemyShip.gameObject.SetActive(false);
            
        }
    }

    void DamageSide(float damage) {
		Debug.Log ("damaged");
        actualXScale = transform.localScale.x;
		if (actualXScale > 0.21f) {
            transform.localScale = new Vector3(actualXScale - damage / 100, transform.localScale.y, transform.localScale.z);
            actualXScale = transform.localScale.x;
		} else {
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            actualXScale = transform.localScale.x;
		}
    }

	void HealtSide(float count){
		Debug.Log ("healed");
		actualXScale = transform.localScale.x;
		if (actualXScale < 0.79f) {
            transform.localScale = new Vector3(actualXScale + count / 100, transform.localScale.y, transform.localScale.z);
            actualXScale = transform.localScale.x;
		} else {
            transform.localScale = Vector3.one;
            actualXScale = transform.localScale.x;
		}
	}
}
