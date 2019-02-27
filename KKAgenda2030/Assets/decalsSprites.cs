using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalsSprites : MonoBehaviour
{

   private WorldManager wGM;

    private void Start()
    {
        wGM = GetComponent<WorldManager>();
    }


    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Decal")
        {
            wGM.decalsSpawn();

            print("Uusi spawni");

        }

    }

}
