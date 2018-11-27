using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

    PhoneVibrate pv;

    private void Start() {
        pv = FindObjectOfType<PhoneVibrate>();
    }

    private void OnTriggerEnter(Collider other) {
        print("enter net");
        if (other.gameObject.tag == "Character") {
            if (pv.nets.Count == 0)
            OceanGameManager.instance.HitNet();
            pv.AddColliderToList(gameObject.GetComponent<Collider>());
            FindObjectOfType<UIManager>().HitAvoidable();
        }
    }

    private void OnTriggerExit(Collider other) {
        print("exit net");
        if (other.gameObject.tag == "Character") {
            pv.RemoveColliderFromList(gameObject.GetComponent<Collider>());
        }
    }
}
