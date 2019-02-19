using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject[] decals;
    public Transform[] decPos;

    
    void Start()
    {
        for (int e = 0; e < decals.Length; e++)
        {
            decalsSpawn();
          
        }

    }

    public void decalsSpawn()
    {

        var dec = Random.Range(0, decals.Length);
        var pos = Random.Range(0, decPos.Length);

        var decs = Instantiate(decals[dec], decPos[pos].position, decPos[pos].rotation);
        print("Spawnaus oli" + dec);
    }
}