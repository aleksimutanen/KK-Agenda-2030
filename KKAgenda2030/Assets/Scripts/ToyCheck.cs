using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCheck : MonoBehaviour {
    
    public GameObject rightObject;
    public bool isReady = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rightObject)
        {
            isReady = true;
            print("Oikea lelu löytyi");

            ToyGameManager.instance.AllToycansFull();

           // rightObject.SetActive(false);

        }
      
    }


    private void OnTriggerExit(Collider other)
    {
        isReady = false;
    }


}
