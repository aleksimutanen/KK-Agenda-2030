using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRepair : MonoBehaviour
{ 
    public List<GameObject> partOfToy;

    private void OnCollisionEnter(Collision col)  
    {
        if (col.gameObject.tag == "RepairCube")
        {
            print("Nyt osuu!");

            partOfToy.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}