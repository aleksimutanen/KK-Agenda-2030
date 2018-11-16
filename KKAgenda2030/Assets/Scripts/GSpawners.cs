using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSpawners : MonoBehaviour
{

    public Transform[] SpawnerPoints;
    public List<GameObject> SpawnerObjects;
    public float spawnStartertime;
    public float resSpawnTimer;
    public float lastSpawn;
    public bool[] spotUsed;

    // Use this for initialization
    void Awake()
    {
        Spawner();

    }

    public void Spawner()
    {
        var rnd = Random.Range(0, SpawnerObjects.Count);


        int spawnPointIndex = Random.Range(0, SpawnerPoints.Length);


        while (spotUsed[spawnPointIndex] == true)
        {
            spawnPointIndex = Random.Range(0, SpawnerPoints.Length);
        }

        Instantiate(SpawnerObjects[rnd], SpawnerPoints[spawnPointIndex].position, SpawnerPoints[spawnPointIndex].rotation);
        SpawnerObjects.RemoveAt(rnd);
        spotUsed[spawnPointIndex] = true;


        print(SpawnerObjects[rnd]);
    }
} 