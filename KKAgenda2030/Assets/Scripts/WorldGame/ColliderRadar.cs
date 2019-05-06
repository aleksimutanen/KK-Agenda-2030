using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRadar : MonoBehaviour
{

    public BoxCollider m_Collider;

    private void Awake()
    {
        m_Collider.size = gameObject.GetComponent<BoxCollider>().size; // Alkuperäinen collider...

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {
            print("Collider size = " + m_Collider.size);
            other.gameObject.GetComponent<BoxCollider>().size = Vector3.one;
            gameObject.GetComponent<BoxCollider>().size = Vector3.zero;
            print("Trigger detected");

        }
    } 



 /*   private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {
            other.gameObject.GetComponent<BoxCollider>().size = Vector3.one;
            gameObject.GetComponent<BoxCollider>().size = Vector3.zero;
        }
    } */


    private void OnTriggerExit(Collider other)
    {
       
            gameObject.GetComponent<BoxCollider>().size = other.gameObject.GetComponent<BoxCollider>().size;
            print(gameObject.GetComponent<BoxCollider>().size);
            print("Trigger left area");


       
    }

}