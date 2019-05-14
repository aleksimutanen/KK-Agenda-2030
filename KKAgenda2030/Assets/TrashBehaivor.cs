using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBehaivor : MonoBehaviour {

    public Animal animal;
    ParticleSystem ps;

    private void Start() {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void TrashClick() {
        ps.Play();
        // play sound
        animal.TrashPicked();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Button>().enabled = false;

    }

}
