using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] rubbish;                
    public float spawnTime = 1f;            
    public Transform[] spawnPoints;         
        
    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {        
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int rubbishObjectsIndex = Random.Range(0, rubbish.Length);
               
        Instantiate(rubbish[rubbishObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);      

    }
}


