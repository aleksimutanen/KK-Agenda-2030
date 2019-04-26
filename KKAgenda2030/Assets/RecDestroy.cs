using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecDestroy : MonoBehaviour
{
    ColliderRadar cR;
    WorldManager wm;
    public GameObject[] decals;
   

    void Start()
    {
        wm = FindObjectOfType<WorldManager>();
        cR = FindObjectOfType<ColliderRadar>();
    }

    void Update()
    {
        decals = GameObject.FindGameObjectsWithTag("Decal");

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);
           
            print("Tuhotaan " + other.gameObject.name);
            print("Objectin tuhosi " + gameObject.name);
        }
    }

    
}