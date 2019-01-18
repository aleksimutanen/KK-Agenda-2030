using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidable : MonoBehaviour {
    public string badFood;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Character") {
            print("trash eaten");
            OceanGameManager.instance.HitTrash();
            gameObject.SetActive(false);
            FindObjectOfType<UIManager>().HitAvoidable();
            FindObjectOfType<PhoneVibrate>().Vibrate();
            FindObjectOfType<CharacterMover>().StartCoroutine("AteTrash");
            Fabric.EventManager.Instance.PostEvent("badFood");
        }    
    }
}
