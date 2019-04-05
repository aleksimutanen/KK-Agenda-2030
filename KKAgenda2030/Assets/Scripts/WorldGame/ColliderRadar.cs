using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRadar : MonoBehaviour
{    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {
            print("Triggeri havaittu");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {
            print("Triggeri poistuu alueelta");

           
        }
    }
    
}