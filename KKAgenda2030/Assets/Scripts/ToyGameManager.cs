using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGameManager : MonoBehaviour {

    public static ToyGameManager instance = null;
    public List<GameObject> kids;
    public List<GameObject> toys;
    public bool levelIsCompleted = false;
    private ToyCheck[] goals;
   

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
                //print(levelIsCompleted);
                //print("bool trying changed");
                levelIsCompleted = true;
                //print(levelIsCompleted);
                //print("bool is changed");
               // print(levelIsCompleted);
            }

            //if (levelIsCompleted == true)
            //{
            //    SceneManager.LoadScene("Menuscene");
            //}

        }
        return !levelIsCompleted;
    }



    // Update is called once per frame
    void Update () {
		
	}
}
