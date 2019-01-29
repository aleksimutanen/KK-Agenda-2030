using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepairManager : MonoBehaviour
{
    public GameObject repairedCube;
    public GameObject readytoy;
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

    void Start()
    {
        repairedCube = GameObject.FindGameObjectWithTag("BrokenToy"); 
        
    }

    void Spawn()
    {
        int bools = Random.Range(0, spotUsed.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnObjectsIndex = Random.Range(0, partOfToys.Count);
       

        while ((spotUsed[spawnPointIndex] == true)) 
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);
            
        }

        var toys =  Instantiate(partOfToys[spawnObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        spotUsed[spawnPointIndex] = true;

        
        
    }

    public void Update()
    {
       if(!repairedCube.activeSelf)
        
        {
            readytoy.SetActive(true);
        }
    }

}