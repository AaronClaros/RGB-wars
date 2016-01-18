using UnityEngine;
using System.Collections;

public class ShipSideScript : MonoBehaviour {
    
    SpriteRenderer sprite;
    public SideColor sideColor;
    
	float actualXScale;
    AmmoGUIScript ammoGUI;

	// Use this for initialization
	void Start () {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ammoGUI = FindObjectOfType(typeof(AmmoGUIScript)) as AmmoGUIScript;
        actualXScale = sprite.transform.localScale.x;
	}

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("impact");
        if (other.gameObject.tag == "Enemy Bullet") {
            var enemyBullet = other.gameObject.GetComponent<EnemyBulletScript>();
            
            if (enemyBullet.color == sideColor) {
                Debug.Log("Parry Bullet");
                enemyBullet.gameObject.SetActive(false);
                ammoGUI.IncreaseAmmoCountGUI(1);
            } else {
                DamageSide(20f);
                Debug.Log("Side Damaged");
                enemyBullet.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.tag == "Enemy Rest"){
            Debug.Log("Rest impacted");
			if (actualXScale < 1) {
				Debug.Log("healed");
				HealtSide(20f);
				other.gameObject.SetActive(false);
			} else {
                Debug.Log("Score Increased");
				GameManager.instance.score += 10;
                other.gameObject.SetActive(false);
			}
        }
        else if (other.gameObject.tag == "Enemy Ship") {
            Debug.Log(other.gameObject.name + " collisioned with " + name);
            var enemyShip = other.gameObject.GetComponent<RedEnemyController>();
            if (enemyShip.shipColor != sideColor)
            {   
                DamageSide(60f);
            }
            else if (enemyShip.shipColor == sideColor)
            {
                AmmoGUIScript.instance.IncreaseAmmoCountGUI(10);
                enemyShip.LeftRest();
            }
            enemyShip.gameObject.SetActive(false);

            Debug.Log(sprite.transform.name + " scaled to " + actualXScale);
        }
    }

    void DamageSide(float damage) {
		Debug.Log ("damaged");
        actualXScale = sprite.transform.localScale.x;
        Debug.Log("side scale " + actualXScale);
		if (actualXScale > 0.21f) {
            sprite.transform.localScale = new Vector3(actualXScale - damage / 100, 1, 1);
            actualXScale = sprite.transform.localScale.x;
		} else {
            sprite.transform.localScale = new Vector3(0, 1, 1);
            actualXScale = sprite.transform.localScale.x;
		}
    }

	void HealtSide(float count){
		Debug.Log ("healed");
        actualXScale = sprite.transform.localScale.x;
		if (actualXScale < 0.79f) {
            sprite.transform.localScale = new Vector3(actualXScale + count / 100, 1, 1);
            actualXScale = sprite.transform.localScale.x;
		} else {
            sprite.transform.localScale = Vector3.one;
            actualXScale = sprite.transform.localScale.x;
		}
	}
}
