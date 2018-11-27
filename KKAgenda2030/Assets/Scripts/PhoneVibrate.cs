using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneVibrate : MonoBehaviour {

    public List<Collider> nets = new List<Collider>();

    void Update() {
        if (nets.Count > 0) Handheld.Vibrate();
    }

    public void AddColliderToList(Collider net) {
        nets.Add(net);
    }

    public void RemoveColliderFromList(Collider net) {
        nets.Remove(net);
    }

    public void Vibrate() {
        Handheld.Vibrate();
    }
}
