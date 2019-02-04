using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spawner : MonoBehaviour
{
    public List<GameObject> rubbish;
    public List<GameObject> rightObjects;
    public List<TrashType> generateTypes;
    public Transform spawnPoint;
    public int sizeOfList = 10;
    public int objectsReqValue  = 5;
    public float spawnTime = 2f;
    public float resSpawnTimer;
    public float lastSpawn;
    public float spawnStartertime;
    public GameObject trashesFolder;


    void Start()
    {
        FilledList();

        Spawn();

    }


    public void Spawn()
    {

        if (spawnStartertime < resSpawnTimer + lastSpawn && rubbish.Count != 1)
        {
            var rnd = Random.Range(0, rubbish.Count);
            var trash = Instantiate(rubbish[rnd], spawnPoint.position, Quaternion.identity);
            var currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Nikle_devscenelvl2") {
                trashesFolder.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            }
            if (currentScene.name == "Nikle_devscenelvl3") {
                trashesFolder.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }
            trash.transform.parent = trashesFolder.transform;
            rubbish.RemoveAt(rnd);
            //print(rnd);
            lastSpawn = Time.time;
            ThrashCountScript.totalThrashCount--;
        }

        //if (rubbish.Count == 0)
        else
        {
            rubbish.Clear();
            StartCoroutine(TrashGameManager.instance.LevelCompleted());
            CancelInvoke("Spawn");
        }
    }

    public void FilledList()
    { 

        // Haetaan sallitut roskatyypit.
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
      //  print("types " + generateTypes.Count);
      //  print(generateTypes[0]);



       // Täytetään lista. Salittujen tapausten mukaan.

        var lenOfList = sizeOfList; // "lenOfList" on listan koko.
        var TrashTyps = generateTypes.Count; // "TrashTyps" on KAIKKI Sallitut roskatyypit. 
        var OneTrashTypeClass = lenOfList / TrashTyps; // "OneTrashTypeClass" on yksittäisen roskatyypin määrä. 
        var FinalTrash = lenOfList - OneTrashTypeClass * (TrashTyps - 1); // "FinalTrash" toteutetaan kun on jäljellä enään viimeinen roskatyyppi.

        // Lisäksi objectsReqValue  on haluttu roskatyypin määrä.
        //   print(lenOfList);
        //   print(TrashTyps);
        //  print(OneTrashTypeClass);
        //   print(FinalTrash);

        for (int W = 0; W < TrashTyps; W++)
        {
            //var generateRubs = FindObjectsOfType<Trash>();
            //foreach( var trueRubs in generateRubs)
            //{

            //}
            int n = (W < TrashTyps - 1) ? OneTrashTypeClass : FinalTrash;
            while (n > 0)
            {
                var rnd1 = Random.Range(0, rightObjects.Count);

                if (rightObjects[rnd1].GetComponent<Trash>().kind == generateTypes[W])
                {

                    if(rightObjects[rnd1].GetComponent<Trash>().kind == TrashType.Biojäte)
                    {


                    }

                    rubbish.Add(rightObjects[rnd1]);               
                    n--;
                }
            }

            // Sekoitetaan lista. Fisher–Yates shuffle algorithm.


            for (int P = 0; P < rubbish.Count; P++)
            {
                GameObject temp = rubbish[P]; // Temp on se jota käydään läpi.
                int randomIndex = Random.Range(P, rubbish.Count); // Valitsee jonkun muun objektin kuin sen jota nyt käydään loopissa läpi.
                rubbish[P] = rubbish[randomIndex]; // Saadaan edellisen rivin tulos.
                rubbish[randomIndex] = temp; // Saadaan edellisen rivin tulos.
                
            }
            
        }
        
    }
}      