using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour {

    PhoneVibrate pv;
    UIManager ui;

    private void Start() {
        pv = FindObjectOfType<PhoneVibrate>();
        ui = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other) {
        print("enter net");
        if (other.gameObject.tag == "Character") {
            if (pv.nets.Count == 0)
            OceanGameManager.instance.HitNet();
            pv.AddColliderToList(gameObject.GetComponent<Collider>());
            ui.HitAvoidable();
        }
    }

    private void OnTriggerExit(Collider other) {
        print("exit net");
        if (other.gameObject.tag == "Character") {
            pv.RemoveColliderFromList(gameObject.GetComponent<Collider>());
        }
    }
}
