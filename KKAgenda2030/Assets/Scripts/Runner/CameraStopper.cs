using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopper : MonoBehaviour
{

    [HideInInspector] public bool parallaxStop = false;

    private void Start() {
        parallaxStop = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            FindObjectOfType<RunnerController>().cameraMove = false;
            parallaxStop = true;
        }

    }
}
