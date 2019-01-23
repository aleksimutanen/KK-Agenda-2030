using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGameManager : MonoBehaviour {

    public static ToyGameManager instance = null;
    public GameObject lightOfRed;
    public List<GameObject> kids;
    public Transform[] kidsPositions;
    public List<GameObject> toys;
    public List<GameObject> repairedToys;
    private ToyCheck[] goals;
    public List<bool> levelIsCompleted;
    public float spawnTime = 2f;
    public float resSpawnTimer;
    public float lastSpawn;
    public float spawnStartertime;



    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            print("ToyGameManager has been found");
        }

        //if not, set instance to this
        instance = this;
        print("ToyGameManager is added to game");

    }


   
    // Use this for initialization
    void Start ()
    {
        goals = FindObjectsOfType<ToyCheck>();  
        lightOfRed.GetComponent<Light>().enabled = false;
        Spawn();
    }

    public bool AllToycansFull()
    {
       
        foreach (var toy in goals)
        {
            if (toy.isReady)
            {
                //print(toy.isReady);
                print(levelIsCompleted);
                print("bool trying changed");
                levelIsCompleted[Random.Range(0, 3)] = true;
                print(levelIsCompleted);
                //print("bool is changed");               
            }

            if (!toy.isReady)
            {
                print("Odottaa että on valmista!");
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

    public void Spawn()
    {
        for (int E = 0; E <= kids.Count; E++)
        {

            var rnd = Random.Range(0, kids.Count);

            Instantiate(kids[rnd]);
            kids.RemoveAt(rnd);
            
            lastSpawn = Time.time;
        }

    }

}   