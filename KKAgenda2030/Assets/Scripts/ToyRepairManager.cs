using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepairManager : MonoBehaviour
{
    ToyRepair tr;
    public List<GameObject> partOfToys;
    public Transform[] spawnPoints;
    public bool[] spotUsed;


    void Awake()
    {

        for (int E = 0; E < spawnPoints.Length; E++)
        {
            Spawn();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tr = FindObjectOfType<ToyRepair>();

    }        

   void Spawn()
    {
        int bools = Random.Range(0, spotUsed.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnObjectsIndex = Random.Range(0, partOfToys.Count);
        
        while((spotUsed[spawnPointIndex] == true)) 
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);

        }

        var toys =  Instantiate(partOfToys[spawnObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        spotUsed[spawnPointIndex] = true;
       

    }
}
