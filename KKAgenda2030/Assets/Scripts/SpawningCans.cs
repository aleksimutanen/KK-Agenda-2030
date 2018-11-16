using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCans : MonoBehaviour {

    private int CansOfSpawn = 2;
    private int ReadySpawning;
    GSpawners spwn;

	// Use this for initialization
	void Start ()
    {
        spwn = FindObjectOfType<GSpawners>();
        Canspawn();
    }
	
	// Update is called once per frame
	void Canspawn ()
    {
        if (ReadySpawning < CansOfSpawn)
        {
            spwn.Spawner();

            ReadySpawning++;
        }
    }


}
