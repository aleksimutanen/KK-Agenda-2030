using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredAreaFilling : MonoBehaviour
{




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Draging>().dragging == true)
        {
            print("Alueelle tulee" + other.gameObject.name);
        }
            
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Draging>().dragging == true)
        {
            print("Alueella on" + other.gameObject.name);

        }

        if (other.GetComponent<Draging>().dragging == false)
        {
            Destroy(other.gameObject);

            print("Alueelta tuhottu" + other.gameObject.name);

        }       


    }
}
