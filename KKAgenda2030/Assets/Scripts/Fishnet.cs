using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishnet : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            print("hit net");
            OceanGameManager.instance.HitNet();
        }
    }
}
