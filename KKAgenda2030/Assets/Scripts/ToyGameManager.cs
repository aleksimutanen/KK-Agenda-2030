using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGameManager : MonoBehaviour {

    public static ToyGameManager instance = null;
    public GameObject lightOfRed;
    public List<GameObject> kids;
    public Transform[] kidsPositions;
    public List<GameObject> toys;
    private ToyCheck[] goals;
    public List<bool> levelIsCompleted;
    public bool[] spotUsed;


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

            for (int e = 0; e < kidsPositions.Length; e++)
            {
                   Spawn();
            }
    }   
    
    void Start ()
    {
        goals = FindObjectsOfType<ToyCheck>();  
        lightOfRed.GetComponent<Light>().enabled = false;
       
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
                lightOfRed.GetComponent<Light>().enabled = false;

            }
            else
            {
               lightOfRed.GetComponent<Light>().enabled = true;
                
            }
        }
        return levelIsCompleted[Random.Range(0,3)];
    }

    void Spawn()
    {
        int bools = Random.Range(0, spotUsed.Length);
        int spawnPointIndex = Random.Range(0, kidsPositions.Length);
        int spawnObjectsIndex = Random.Range(0, kids.Count);


        while ((spotUsed[spawnPointIndex] == true))
        {
            spawnPointIndex = Random.Range(0, kidsPositions.Length);

        }

        var childs = Instantiate(kids[spawnObjectsIndex], kidsPositions[spawnPointIndex].position, kidsPositions[spawnPointIndex].rotation);
        kids.RemoveAt(spawnObjectsIndex);
        spotUsed[spawnPointIndex] = true;

    }

}   