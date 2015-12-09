using UnityEngine;
using System.Collections;

public class ShipSideScript : MonoBehaviour {
    
    SpriteRenderer sprite;
    public colorGame sideColor;
    
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("impact");
        if (other.gameObject.tag == "Enemy Bullet") {
            var enemyBullet = other.gameObject.GetComponent<EnemyBulletScript>();
            
            if (enemyBullet.color == sideColor) {
                Debug.Log("Parry Bullet");
                enemyBullet.gameObject.SetActive(false);
            } else {
                DamageSide(5f);
                Debug.Log("Side Damaged");
                enemyBullet.gameObject.SetActive(false);
            }
        }
        else {
            Debug.Log("no bullet impact");
        }
    }

    void DamageSide(float damage) {
        if (sprite.color.a >= 0 ) {
            float actualAlfa = sprite.color.a;
            sprite.color = new Color(255f, 255f, 255, actualAlfa - damage);
            actualAlfa = sprite.color.a;
        }
    }
}
