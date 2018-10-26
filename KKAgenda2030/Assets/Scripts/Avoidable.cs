using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidable : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            print("trash eaten");
            OceanGameManager.instance.AddTrashToList(this);
            gameObject.SetActive(false);
        }    
    }
}
