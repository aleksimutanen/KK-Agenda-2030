using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepairManager : MonoBehaviour
{

    ToyRepair tR;
    public GameObject fullrepairedCube;
    private GameObject repairedCube;
    public GameObject readytoy;
    public List<GameObject> partOfToys;
    public Transform[] spawnPoints;
    public bool[] spotUsed;
    public bool[] isRepaired;
    

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

    public void Update()
    {
        repairedIsReady();
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
        partOfToys.RemoveAt(spawnObjectsIndex);
        spotUsed[spawnPointIndex] = true;        
        
    }

    void repairedIsReady()
    {
        int repairedBool = Random.Range(0, isRepaired.Length);
        int i = 0;

        while (i > isRepaired.Length)
        {    

            if (!repairedCube.activeSelf)
            {
            isRepaired[repairedBool] = true;
            print("Lelu on piilossa");
            }
            i++;
        }
    }
}