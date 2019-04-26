using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRadar : MonoBehaviour
{

  public  BoxCollider m_Collider;



    

    private void Awake()
    {
        m_Collider.size = gameObject.GetComponent<BoxCollider>().size;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {
            print("Colliderin koko on " + m_Collider.size);
           // other.gameObject.GetComponent<BoxCollider>().size = Vector3.one;
            gameObject.GetComponent<BoxCollider>().size = Vector3.zero;
            print("Triggeri havaittu");

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains(gameObject.name))
        {

            gameObject.GetComponent<BoxCollider>().size = other.gameObject.GetComponent<BoxCollider>().size;
            print(gameObject.GetComponent<BoxCollider>().size);
            print("Triggeri poistuu alueelta");


        }
    }

}