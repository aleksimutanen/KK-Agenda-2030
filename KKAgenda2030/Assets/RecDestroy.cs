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



    void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Decal")
        {
            Destroy(collision.gameObject);

            print("Tuhotaan " + collision.gameObject.name);
        }

    }
}