using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public int enemiesPerSpawnTime = 3;
    public float timeBetweenSpawn = 2;
    public int score = 0;
    [HideInInspector]
    private ObjectPool enemiesPool1;
    [HideInInspector]
    private ObjectPool enemiesPool2;
    [HideInInspector]
    private ObjectPool enemiesPool3;
    [HideInInspector]
    private ObjectPool[] poolsList;

    // Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null) {
            Destroy(this);
        }

        Setup();
	}

    void Setup() {
        poolsList = GetComponents<ObjectPool>();
    }

    IEnumerator SpawnEnemy(int countPerSecond) {
        yield return new WaitForSeconds(timeBetweenSpawn);
        Vector2 RandomPosition = Vector2.zero;
        Debug.Log(Screen.width + " - " + Screen.height);
        for (int i = 0; i < countPerSecond; i++)
        {
            RandomPosition = new Vector2(40f, Random.Range(-14f, 18f));
            GameObject newEnemy = RandomPoolInstance().NextObject();
            newEnemy.transform.position = RandomPosition;
            newEnemy.SetActive(true);
        }
        StopAllCoroutines();
    }

    ObjectPool RandomPoolInstance() {
        int poolIndex = (int)(Random.Range(0f, 2.99f));
        Debug.Log("enemie index :"+poolIndex);
        ObjectPool poolToUse = poolsList[poolIndex];
        return poolToUse;
    }
	
	// Update is called once per frame
	void Update () {
        //Spawing Enemies
        StartCoroutine(SpawnEnemy(enemiesPerSpawnTime));
	}
}
