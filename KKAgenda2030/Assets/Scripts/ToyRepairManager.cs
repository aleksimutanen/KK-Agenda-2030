using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ToyRepairManager : MonoBehaviour {

    ToyRepair tR;
    Toy toytype;
    public GameObject toysFolder;
    public List<ToyType> generateToysTypes;
    public GameObject repairCube;
    private GameObject repairedCube;
    public List<GameObject> almostReadytoys;
    public List<GameObject> allPartOfToys;
    public List<GameObject> partOfToys;
    public List<GameObject> toysCollected;
    public Transform[] spawnPoints;
    public Transform[] goalPoints;
    public int sizeOfList = 3;
    public bool[] spotUsed;
    public bool[] isRepaired;
    int isCollected = 0;

    public GameObject carSilhuette;
    public GameObject nalleSilhuette;
    public GameObject planeSilhuette;

    public Animator lennuAnimator;

    public int toysInPlace;


    void Awake() {

        tR = GameObject.FindObjectOfType<ToyRepair>();

        FilledList();

        for (int E = 0; E < spawnPoints.Length; E++) {
            Spawn();
        }
    }

    public void UseToyPart(GameObject toy) {
        if (isRepaired.Length - 1 >= isCollected) {
            isRepaired[isCollected] = true;
            isCollected++;
            //print(isCollected);
        }
    }

    void Spawn() {
        int bools = Random.Range(0, spotUsed.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnObjectsIndex = Random.Range(0, partOfToys.Count);

        while ((spotUsed[spawnPointIndex] == true)) {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);
        }

        var toys = Instantiate(partOfToys[spawnObjectsIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        var toyKind = toys.GetComponent<Toy>().kind;
        if (toyKind == ToyType.Car) {
            carSilhuette.SetActive(true);
        }
        if (toyKind == ToyType.Nalle) {
            nalleSilhuette.SetActive(true);
        }
        if (toyKind == ToyType.Plane) {
            planeSilhuette.SetActive(true);
        }
        toys.transform.parent = toysFolder.transform;
        partOfToys.RemoveAt(spawnObjectsIndex);
        spotUsed[spawnPointIndex] = true;
    }

    void FilledList() {

        // Täytetään lista. Salittujen tapausten mukaan. Joita on aina 3 per lelu.

        //   var lenOfList = sizeOfList; // "lenOfList" on listan koko.
        //   var OneTrashTypeClass = lenOfList / generateToysTypes.Count; // "OneTrashTypeClass" on yksittäisen lelutyypin määrä. 
        //   var FinalTrash = lenOfList - OneTrashTypeClass * (generateToysTypes.Count - 1); // "FinalTrash" toteutetaan kun on jäljellä enään viimeinen roskatyyppi.

        //for (int W = 0; W < generateToysTypes.Count; W++) {
        //    int n = (W < generateToysTypes.Count - 1) ? OneTrashTypeClass : FinalTrash;
        //    while (n > 0) {
        //        var rnd1 = Random.Range(0, allPartOfToys.Count);

        //        if (allPartOfToys[rnd1].GetComponent<Toy>().kind == ToyType.Car /*||  allPartOfToys[rnd1].GetComponent<Toy>().kind == ToyType.Car 
        //            ||  allPartOfToys[rnd1]GetComponent<Toy>().kind == ToyType.Car */)  // Katsoo kaikki 3 tietyn tyypin lelut.
        //          {
        //           partOfToys.Add(allPartOfToys[rnd1]); // Lisää lelut listaan josta ne luodaan peliin.
        //           allPartOfToys.RemoveAt(rnd1);
        //           n--;
        //          }
        //    }
        //}

        var typ = generateToysTypes[Random.Range(0, generateToysTypes.Count)];
        for (int i = 0; i < allPartOfToys.Count; i++) {
            if (allPartOfToys[i].GetComponent<Toy>().kind == typ) {
                partOfToys.Add(allPartOfToys[i]); // Lisää lelut listaan josta ne luodaan peliin.
                allPartOfToys.RemoveAt(i);
                i--;
            }
        }

    }

    public void RepairedIsReady() {
        bool allRepaired = true;
        foreach (var t in isRepaired) {
            if (!t) {
                allRepaired = false;
                break;
            }
            //  allRepaired = isRepaired.Any(x => x); Tarkoittaa samaa kuin yllä oleva.
        }

        if (allRepaired) {
            //  readytoy.SetActive(true);
            repairCube.SetActive(false);
        }
    }
}