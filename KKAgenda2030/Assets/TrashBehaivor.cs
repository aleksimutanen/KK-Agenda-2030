using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaivor : MonoBehaviour {

    public Animal animal;
    public string happy_animation;

    void Start() {

    }

    void Update() {

    }

    public void TrashClick() {
        // toggle object off
        // play particles
        // play sound
        gameObject.SetActive(false);


        animal.trashesLeft--;
        if (animal.trashesLeft == 0) {
            animal.GetComponent<Animator>().Play(happy_animation);
        }
    }

}
