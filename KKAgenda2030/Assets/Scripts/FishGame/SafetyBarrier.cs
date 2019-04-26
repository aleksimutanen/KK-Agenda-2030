using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyBarrier : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            var b  = FindObjectOfType<CharacterMover>();
            b.transform.position = b.hitPosition;
        }
    }
}
