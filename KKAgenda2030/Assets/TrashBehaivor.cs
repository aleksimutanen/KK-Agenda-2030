using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaivor : MonoBehaviour {

    public Animal animal;
    public string happy_animation;


    public void TrashClick() {
        gameObject.SetActive(false);
        // play particles
        // play sound
        animal.trashesLeft--;
        if (animal.trashesLeft == 0) {
            animal.GetComponent<AnimatorTimer>().enabled = false;
            animal.GetComponent<Animator>().Play(happy_animation);
        }
    }

}
