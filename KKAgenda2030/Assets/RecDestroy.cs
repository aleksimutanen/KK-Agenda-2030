using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecDestroy : MonoBehaviour
{
    WorldManager wm;
    public GameObject[] decals;
   

    void Start()
    {
        wm = FindObjectOfType<WorldManager>();
    }

    void Update()
    {
        decals = GameObject.FindGameObjectsWithTag("Decal");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);

            print("Tuhotaan " + other.gameObject.name);
        }
    }

    
}