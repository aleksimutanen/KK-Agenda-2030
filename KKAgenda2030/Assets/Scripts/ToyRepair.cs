using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    



    private void Start()
    {
        gameObject.SetActive(true);
        tRM = GameObject.FindObjectOfType<ToyRepairManager>();
        
    }

    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
            //  print("Nyt osuu!");

            gameObject.SetActive(false);
            tRM.toysCollected.Add(gameObject);
            tRM.UseToyPart(this.gameObject);
                         
        }

       

    }
   
}