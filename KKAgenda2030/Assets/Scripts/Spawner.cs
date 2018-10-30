using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] rubbish;
    private GameObject[] rubs;
    public Transform spawnPoint;
    public float spawnTime = 2f;
    public float resSpawnTimer = 0f;
    public float lastSpawn;
    private int rubIndex;
    

    
    void Start()
    {       
        InvokeRepeating("Spawn", spawnTime, resSpawnTimer);
    }



    public void Spawn()
    {
        rubs = new GameObject[rubbish.Length];


        for (int i = 0; i < rubbish.Length; i++)
        {
           // rubs[i] = Instantiate(rubbish[Random.Range(0, 2)]) as GameObject;

            rubs[i] = Instantiate(rubbish[i]) as GameObject;
            

            if (Time.time > resSpawnTimer + lastSpawn)
            {
              // rubs[i] = Instantiate(rubbish[Random.Range(0, rubbish.Length)]) as GameObject;
               
                

               // print(rubs[i]);
               print(i);
               lastSpawn = Time.time;
            }
            else
            {
               CancelInvoke("Spawn");
            }
            
        }

    }

    //void Update()
    //{
    //    Spawn();
    //}

}






