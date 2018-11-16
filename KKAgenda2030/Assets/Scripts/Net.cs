using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            print("hit net");
            OceanGameManager.instance.HitNet();
            FindObjectOfType<UIManager>().HitAvoidable();
        }
    }
}
