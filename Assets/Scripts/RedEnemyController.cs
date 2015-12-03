using UnityEngine;
using System.Collections;

public class RedEnemyController : MonoBehaviour {
	public float reloadTime = 2f;
	public float fireRate = 3f;
	float lastfired;
	float lastReload;
	public bool reloading;
	EnemyBulletPool pool;
	// Use this for initialization
	void Start () {
		pool = GetComponent<EnemyBulletPool> ();
		lastfired = 0;
		lastReload = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!reloading) {
			if (Time.time - lastfired > 1 / fireRate)
			{
				lastfired = Time.time;
				var bullet = pool.NextObject();
				bullet.transform.position = transform.position;
				bullet.SetActive(true);
			}
		}
		//Falta implemetar recarga de arma
		if (Time.time > lastReload + reloadTime) {
			reloading = !reloading;
		}
	}
}
