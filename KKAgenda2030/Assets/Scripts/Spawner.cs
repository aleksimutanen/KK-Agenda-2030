using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject AOS;
    public GameObject player;
    public GameObject[] rubbish;
    public float spawnTime = 1f;
    //public float reSpawnTime = 3f;
    public Transform[] spawnPoints;
    public bool isEmpty;
 

    // Use this for initialization
    void Start()
    {
        //AOS = GetComponent<DragObjects>().OnMouseDrag();
        player.GetComponent<DragObjects>().OnMouseDrag();
        InvokeRepeating("Spawn", spawnTime,spawnTime);
        isEmpty = false;
    }

    public void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int rubbishObjectsIndex = Random.Range(0, rubbish.Length);

        Instantiate(rubbish[rubbishObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        isEmpty = true;
               
        //if () 
        //{
        //    Instantiate(rubbish[rubbishObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        //    isEmpty = true;
        //}

        //else

        //{
        //    isEmpty = false;
        //} 
      
    }

   /* void ReSpawn(Transform[] spawnPoints)
    {

        if (rubbish == null)
        {
            isEmpty = false;
            print("On tyhjänä");
        } 
    }

    private void FixedUpdate()
    {
        ReSpawn(spawnPoints);
    } */
}




