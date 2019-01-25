using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public GameObject toy;
    private int objectsfalse = 0;


    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
            print("Nyt osuu!");
          
            gameObject.SetActive(false);
;
        }

        if (!gameObject.activeSelf)
        {
            print("objekti ei näy!");
            tRM.spotUsed[objectsfalse] = true;
            toy.SetActive(true);               
        }
    }
}