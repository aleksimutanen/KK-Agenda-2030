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

    private void OnTriggerEnter2D(Collider2D other)
    {

        print(other.name);
        if (other.gameObject.tag == "Decal")
        {
            Destroy(other.gameObject);
           
           
            print("Objectin tuhosi " + gameObject.name);
        }
    }

    
}