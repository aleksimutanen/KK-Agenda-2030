using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject decalsFolder;

    public List<GameObject> decals;
    public Transform[] decPositions;
   


    void Start()
    {

        for (int i = 0; i < decals.Count + 5;)
        {

            decalsSpawn();
            i++;
        }


    }

    public void decalsSpawn()
    {
        
          
            var rnd = Random.Range(0, decals.Count);
            var pos = Random.Range(0, decPositions.Length);

         

            var decs = Instantiate(decals[rnd], decPositions[pos].position, Quaternion.identity);
            decs.transform.parent = decalsFolder.transform;
            decals.RemoveAt(rnd);
          
    }
}