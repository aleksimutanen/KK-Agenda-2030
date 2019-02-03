using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToyRepairManager : MonoBehaviour
{

    ToyRepair tR;
    public GameObject AllmoustRepairedCube;
    private GameObject repairedCube;
    public GameObject readytoy;
    public List<GameObject> partOfToys;
    public Transform[] spawnPoints;
    public bool[] spotUsed;
    public bool[] isRepaired;
    int isCollected = 0;


    public void UseToyPart(GameObject toy)
    {

        if (isRepaired.Length - 1 >= isCollected)
        {

            isRepaired[isCollected] = true;
            isCollected++;
            print(isCollected);
        }

        repairedIsReady();

    }

    void Awake()
    {

        for (int E = 0; E < spawnPoints.Length; E++)
        {
            Spawn();
        }
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

   public void repairedIsReady() 
   {
        bool allRepaired = true;
        foreach (var t in isRepaired)
        {
            if (!t)
            {
               
                allRepaired = false;
               
               
                break;                
            }

            //  allRepaired = isRepaired.Any(x => x); Tarkoittaa samaa kuin yllä oleva.
        }

        if (allRepaired)
        {
            readytoy.SetActive(true);

        }

        

    }
}