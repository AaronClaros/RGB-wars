using UnityEngine;
using System.Collections;

public class RedEnemyController : MonoBehaviour {
    public float moveSpeed = 0.3f;
    public int ammoCount = 5;
    public float reloadTime = 2f;
	public float fireRate = 10f;
    int actualAmmo;
	float lastfired;
	float lastReload;
	public bool reloading;
	EnemyBulletPool pool;
	// Use this for initialization
	void Start () {
		pool = GetComponent<EnemyBulletPool> ();
        actualAmmo = ammoCount;
		lastfired = 0;
		lastReload = 0;
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(Shoot(reloadTime));
	}
    
    IEnumerator Shoot(float rT) {
        if (!reloading) {
            if (actualAmmo > 0)
            {
                if (Time.time - lastfired > 1 / fireRate)
                {
                    lastfired = Time.time;
                    var bullet = pool.NextObject();
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                    actualAmmo--;
                }
            }
            else {
                reloading = true;
            }
        }
        yield return new WaitForSeconds(rT);
        actualAmmo = ammoCount;
        reloading = false; ;
        StopAllCoroutines();
    }
}
