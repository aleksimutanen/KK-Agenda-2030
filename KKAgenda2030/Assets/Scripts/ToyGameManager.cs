using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGameManager : MonoBehaviour {

    public static ToyGameManager instance = null;
    public List<GameObject> kids;
    public List<GameObject> toys;
    private ToyCheck[] goals;
    public List<bool> levelIsCompleted;
   

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
        
	}

    public bool AllToycansFull()
    {
        // levelIsCompleted = false;

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

                if (levelIsCompleted[Random.Range(0, 3)] == true)
                {
                    
                }



            }


        }
        return levelIsCompleted[Random.Range(0,3)];
    }



    // Update is called once per frame
    void Update () {
		
	}
}
