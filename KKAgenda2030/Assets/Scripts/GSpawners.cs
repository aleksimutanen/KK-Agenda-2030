using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSpawners : MonoBehaviour {

    public List<Transform> SpawnerPoints;
    public List<GameObject> SpawnerObjects;
    public float spawnStartertime;
    public float resSpawnTimer;
    public float lastSpawn;

    // Use this for initialization
    void Start ()
    {
        Spawn();
		
	}
	
	// Update is called once per frame
	void Update ()
    {

		
	}

    public void Spawn()
    {
        if (spawnStartertime < resSpawnTimer + lastSpawn)
        {
            var rnd = Random.Range(0, SpawnerObjects.Count);
            var PosRound = Random.Range(0, SpawnerPoints.Count);


            // Find a random index between zero and one less than the number of spawn points.
            // int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            // Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        

           // Instantiate(SpawnerObjects[rnd], PosRound.position, Quaternion.identity);           
           // SpawnerObjects.RemoveAt(rnd);


            print(rnd);
            lastSpawn = Time.time;
        }

    }
}
