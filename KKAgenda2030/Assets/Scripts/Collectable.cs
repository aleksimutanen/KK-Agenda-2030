using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            print("food found");
            OceanGameManager.instance.HitFood();
            gameObject.SetActive(false);
            FindObjectOfType<CharacterMover>().GrowScale();
            FindObjectOfType<UIManager>().HitFood();
        }
    }
}
