using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    ToyRepairManager tRM;
    public GameObject toy;


    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
            print("Nyt osuu!");
          
            gameObject.SetActive(false);
            tRM.partOfToys.Remove(this.gameObject);
        }
    }
}