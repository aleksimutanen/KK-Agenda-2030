using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnter : MonoBehaviour
{
    public GameObject originalObject;
    public BoxCollider originalCollider;

    private void Awake()
    {
        originalCollider.size = originalObject.GetComponent<BoxCollider>().size; // Alkuperäinen collider...
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Clone"))
        {
            originalObject.GetComponent<BoxCollider>().enabled = false;
            print("Triggeri havaittu");
        }
    }


}
