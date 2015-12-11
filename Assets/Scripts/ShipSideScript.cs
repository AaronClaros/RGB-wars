using UnityEngine;
using System.Collections;

public class ShipSideScript : MonoBehaviour {
    
    SpriteRenderer sprite;
    public colorGame sideColor;
    
	float actualAlfa;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("impact");
        if (other.gameObject.tag == "Enemy Bullet") {
            var enemyBullet = other.gameObject.GetComponent<EnemyBulletScript>();
            
            if (enemyBullet.color == sideColor) {
                //Debug.Log("Parry Bullet");
                enemyBullet.gameObject.SetActive(false);
            } else {
                DamageSide(20f);
                //Debug.Log("Side Damaged");
                enemyBullet.gameObject.SetActive(false);
            }
        }
        else {
            //Debug.Log("no bullet impact");
        }
    }

    void DamageSide(float damage) {
		Debug.Log ("damaged");
		actualAlfa = sprite.color.a;
		if (actualAlfa > 0.21f) {
			sprite.color = new Color (255f, 255f, 255f, actualAlfa - damage / 100);
			actualAlfa = sprite.color.a;
		} else {
			sprite.color = new Color (255f, 255f, 255f, 0.1f);
			actualAlfa = sprite.color.a;
		}
    }
}
