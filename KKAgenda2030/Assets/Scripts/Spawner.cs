using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject AOS;
    public GameObject player;
    public GameObject[] rubbish;
    public float spawnTime = 1f;
    
    public Transform[] spawnPoints;
    public float draggingtimer = 0.5f;
    public bool isEmpty;
 

    // Use this for initialization
    void Start()
    {        
        InvokeRepeating("Spawn", spawnTime,spawnTime);
        isEmpty = false;
    }

    public void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int rubbishObjectsIndex = Random.Range(0, rubbish.Length);

        //Instantiate(rubbish[rubbishObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        //isEmpty = true;

        if (Input.GetKey(KeyCode.Mouse0))           
        {

            isEmpty = true;
            player.GetComponent<DragObjects>().OnMouseDrag();

            if (isEmpty == true)
            {
                Instantiate(rubbish[rubbishObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                draggingtimer -= Time.deltaTime; 
            }

        }

        else

        { 
            isEmpty = false;
        }

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




