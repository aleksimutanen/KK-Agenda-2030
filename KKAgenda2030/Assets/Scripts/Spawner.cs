using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> rubbish;
    public Transform spawnPoint;
    public float spawnTime = 2f;
    public float resSpawnTimer = 0f;
    public float lastSpawn;
    

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, resSpawnTimer);
    }



    public void Spawn()
    {

        if (Time.time > resSpawnTimer + lastSpawn)
        {
            var rnd = Random.Range(0, rubbish.Count);
            Instantiate(rubbish[rnd]);
            rubbish.RemoveAt(rnd);

            print(rnd);
            lastSpawn = Time.time;
        }



        if (rubbish.Count.Equals(0))
        {
            print("Lista on tyhjä");

          // TrashGameManager.instance.AllTrashcansFull();


            CancelInvoke("Spawn");
        }

    }




}


