using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCans : MonoBehaviour {

    public int[] CansOfSpawn;
    private int ReadySpawning;
    GSpawners spwn;

	// Use this for initialization
	void Awake ()
    {
        spwn = FindObjectOfType<GSpawners>();
        
        Canspawn();
    }
	
	// Update is called once per frame
	void Canspawn ()
    {
        
        if (ReadySpawning < CansOfSpawn[0])
        {
            spwn.Spawner();

            ReadySpawning++;
        }


    }


}
