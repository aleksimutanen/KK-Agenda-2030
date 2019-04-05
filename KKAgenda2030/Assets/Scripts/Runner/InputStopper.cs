using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStopper : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            var c = FindObjectOfType<RunnerController>();
            c.canMove = false;
            //c.hitWeb = true;
            print("input stopped");
        }
    }
}
