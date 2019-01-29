using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public int objectsfalse = 0;

    private void Start()
    {
      gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
           // print("Nyt osuu!");
          
            gameObject.SetActive(false);
            objectsfalse++;

        }

        if (!gameObject.activeSelf)
        {
          //  print("objekti ei näy!");
         
                    
        }
    }
}