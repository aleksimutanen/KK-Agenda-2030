using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> rubbish;
    public List<GameObject> rightObjects;
    public List<TrashType> generateTypes;
    public Transform spawnPoint;
    public int sizeOfList = 10;
    public float spawnTime = 2f;
    public float resSpawnTimer;
    public float lastSpawn;
    public float spawnStartertime;
    GSpawners spwn;


    void Start()
    {
        FilledList();

        spwn = FindObjectOfType<GSpawners>();
        Spawn();
    }



    public void Spawn()
    {

        if (spawnStartertime < resSpawnTimer + lastSpawn)
        {

            var rnd = Random.Range(0, rubbish.Count);

            Instantiate(rubbish[rnd]);
            rubbish.RemoveAt(rnd);
            

            //print(rnd);
            lastSpawn = Time.time;
        }

        if (rubbish.Count.Equals(0))
        {
            print("Lista on tyhjä");

            TrashGameManager.instance.LevelCompleted();
            CancelInvoke("Spawn");
        }


    }

    public void FilledList()
    {
       
        // Täytetään roskalista halutuilla objekteilla.
        
        var trashcans = FindObjectsOfType<TrashDestroy>();
        generateTypes = new List<TrashType>();
        foreach (var can in trashcans)
        {
            foreach (var typ in can.acceptTypes)
            {
                if (!generateTypes.Contains(typ))
                    generateTypes.Add(typ);
            }
        }
        print("types " + generateTypes.Count);

             

        for (int M = 0; M < sizeOfList;)
        {
            var rnd = Random.Range(0, rightObjects.Count);
            if (generateTypes.Contains(rightObjects[rnd].GetComponent<Trash>().kind))
            {
                rubbish.Add(rightObjects[rnd]);
                M++;
                print("Roskia lisätty listaan");
            }
            //rubbish.Add(rightObjects[0]);
           // print(rubbish.Count);
        }

    }
}      