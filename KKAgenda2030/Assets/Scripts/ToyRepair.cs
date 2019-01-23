using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{

    public List<bool> levelIsCompleted;


    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
            print("Nyt osuu!");

            gameObject.SetActive(false);
            
        }
    }
}