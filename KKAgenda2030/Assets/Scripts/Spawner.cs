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
    public int ObjectsReq = 5;
    public float spawnTime = 2f;
    public float resSpawnTimer;
    public float lastSpawn;
    public float spawnStartertime;


    void Start()
    {
        FilledList();

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
            print("List is empty");

            TrashGameManager.instance.LevelCompleted();
            CancelInvoke("Spawn");
        }


    }

    public void FilledList()
    {
       
        // Filled Trashlist Trash Objects.
        
        // Find trash accepted types.
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

        // GenerateTypes = 2.

        // Two Trash object can added to list.

        // Above sizeofList is Max Value list. This list is 10.     

        for (int M = 0; M < sizeOfList;)
        {
            var rnd = Random.Range(0, rightObjects.Count);
            if (generateTypes.Contains(rightObjects[rnd].GetComponent<Trash>().kind))
            {
                rubbish.Add(rightObjects[rnd]);
                print(rnd);
                M++;
                print("Trash added list");
            }


            // And now i want check max value per Objects = 5!
            // Examble 5 Metal trash and 5 Glass Trash.
            // but how i doing it?
            if (rubbish.Contains(rightObjects[rnd])) 
            {
                if (rubbish.Count > ObjectsReq)
                {
                    rubbish.Remove(rightObjects[rnd]);
                    print("Poistetaan objekti listasta");
                } 


            }
           

        }

    }
}      