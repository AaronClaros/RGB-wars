using UnityEngine;
using System.Collections;

public class RedEnemyController : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public int ammoCount = 5;
    public float reloadTime = 2f;
    public float fireRate = 10f;
    int actualAmmo;
    float lastfired;
    public bool reloading;
    EnemyBulletPool pool;

    public SideColor shipColor;

    public GameObject restPrefab;
    SpriteRenderer sprite;
    float actualAlfa;
    // Use this for initialization
    void Start()
    {
        pool = GetComponent<EnemyBulletPool>();
        actualAmmo = ammoCount;
        lastfired = 0;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Shoot(reloadTime));
        transform.position = new Vector2(transform.position.x - (1 * moveSpeed), transform.position.y);
    }

    IEnumerator Shoot(float rT)
    {
        if (!reloading)
        {
            if (actualAmmo > 0)
            {
                if (Time.time - lastfired > 1 / fireRate)
                {
                    lastfired = Time.time;
                    var bullet = pool.NextObject();
					bullet.transform.position = new Vector2(transform.position.x -1.5f, transform.position.y);
                    bullet.SetActive(true);
                    actualAmmo--;
                }
            }
            else
            {
                reloading = true;
            }
        }
        yield return new WaitForSeconds(rT);
        actualAmmo = ammoCount;
        reloading = false; ;
        StopAllCoroutines();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Bullet") {
			var playerBullet = other.gameObject.GetComponent<PlayerBulletScript> ();
			if (shipColor == playerBullet.color) {
				//Debug.Log ("Enemy Ship Parry Bullet");
			} else {
                DamageEnemy(20f);
				//Debug.Log ("Enemy destroyed");
			}
		}
    }

	void OnCollisionEnter2D(Collision2D other){

	}

	IEnumerator MakeCollisionable(float time){
		yield return new WaitForSeconds (time);
	}

    public void LeftRest() {
        var rest = Instantiate(restPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        rest.transform.position = transform.position;
        rest.SetActive(true);
    }

    void DamageEnemy(float damage) {
        //Debug.Log("damaged");
        actualAlfa = sprite.color.a;
        if (actualAlfa > 0.21f)
        {
            sprite.color = new Color(255f, 255f, 255f, actualAlfa - damage / 100);
            actualAlfa = sprite.color.a;
        }
        else
        {
            gameObject.SetActive(false);
            LeftRest();
        }
    }
}
