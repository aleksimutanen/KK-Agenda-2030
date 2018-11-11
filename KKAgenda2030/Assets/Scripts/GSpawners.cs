using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSpawners : MonoBehaviour {

    public Transform[] SpawnerPoints;
    public List<GameObject> SpawnerObjects;
    public float spawnStartertime;
    public float resSpawnTimer;
    public float lastSpawn;

    // Use this for initialization
    void Start ()
    {
        Spawner();

        CancelInvoke("Spawner");
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        

    }

    public void Spawner()
    {
        if (spawnStartertime < resSpawnTimer + lastSpawn)
        {
           var rnd = Random.Range(0, SpawnerObjects.Count);
           int spawnPointIndex = Random.Range(0, SpawnerPoints.Length);
        
           Instantiate(SpawnerObjects[rnd], SpawnerPoints[spawnPointIndex].position, SpawnerPoints[spawnPointIndex].rotation);           
           SpawnerObjects.RemoveAt(rnd);
          

           print(rnd);
           lastSpawn = Time.time;
        }

    }
}
