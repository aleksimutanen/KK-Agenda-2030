using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalsSprites : MonoBehaviour
{

   private WorldManager wGM;

    void Start()
    {
       
        wGM = FindObjectOfType<WorldManager>();
    }


    void OnCollisionEnter(UnityEngine.Collision hit)
    {
        if (hit.gameObject.tag == "Decal")
        {
            print("osuu " + hit.gameObject.name);

            //    wGM.decals.Add(hit.gameObject);
            print("Lisätty listaan " + hit.gameObject.name);

        }

        int i = 0;
        /*    if(wGM.decals.Count > i)
            {
                wGM.decalsSpawn();
                print("installoidaan " + hit.gameObject.name);
            }*/
    }

}
