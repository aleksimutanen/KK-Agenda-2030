using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGameManager : MonoBehaviour {

    public static ToyGameManager instance = null;
    public GameObject toysFolder;
    public List<GameObject> kids;
    public Transform[] hintsPositions;
    public List<GameObject> toys;
    public Transform[] toysPositions;
    private ToyCheck[] goals;
    public List<bool> levelIsCompleted;
    public bool[] spotUsed;
    public bool[] toysSpotUsed;



    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
      //      print("ToyGameManager has been found");
        }

        //if not, set instance to this
        instance = this;
    //    print("ToyGameManager is added to game");

            for (int e = 0; e < hintsPositions.Length; e++)
            {
                   hintsSpawn();
            }



            for (int i = 0; i < toys.Count +2; i++)
            {
           
                toysSpawn();
                
            }
    }   
    
    void Start ()
    {
        goals = FindObjectsOfType<ToyCheck>();  
      
    }

    public bool AllToycansFull()
    {
       
        foreach (var toy in goals)
        {
            if (toy.isReady)
            {
              
                levelIsCompleted[Random.Range(0, 3)] = true;
                     
            }

            if (!toy.isReady)
            {
            //    print("Odottaa että on valmista!");
                // lightOfRed.SetActive(false);
             //   lightOfRed.GetComponent<Light>().enabled = false;

            }
            else
            {
              // lightOfRed.GetComponent<Light>().enabled = true;
                
            }
        }
        return levelIsCompleted[Random.Range(0,3)];
    }

    void hintsSpawn()
    {
        int bools = Random.Range(0, spotUsed.Length);
        int spawnPointIndex = Random.Range(0, hintsPositions.Length);
        int spawnObjectsIndex = Random.Range(0, kids.Count);


        while ((spotUsed[spawnPointIndex] == true))
        {
            spawnPointIndex = Random.Range(0, hintsPositions.Length);

        }

        var childs = Instantiate(kids[spawnObjectsIndex], hintsPositions[spawnPointIndex].position, hintsPositions[spawnPointIndex].rotation);
        childs.transform.parent = toysFolder.transform;
        kids.RemoveAt(spawnObjectsIndex);
        spotUsed[spawnPointIndex] = true;

    }

    void toysSpawn()
    {
        int bools = Random.Range(0, toysSpotUsed.Length);
        int spawnPointIndex = Random.Range(0, toysPositions.Length);
        int spawnObjectsIndex = Random.Range(0, toys.Count);


        while ((toysSpotUsed[spawnPointIndex] == true))
        {
            spawnPointIndex = Random.Range(0, toysPositions.Length);

        }

        var toy = Instantiate(toys[spawnObjectsIndex], toysPositions[spawnPointIndex].position, toysPositions[spawnPointIndex].rotation);
        toy.transform.parent = toysFolder.transform;
        toys.RemoveAt(spawnObjectsIndex);
        toysSpotUsed[spawnPointIndex] = true;

    }

}   