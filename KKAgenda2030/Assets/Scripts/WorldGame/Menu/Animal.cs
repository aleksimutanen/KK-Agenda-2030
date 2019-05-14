using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {

    public int trashesLeft;
    public string happy_animation;


    void Start() {
        trashesLeft = FindObjectsOfType<TrashBehaivor>().Count(t => t.animal == this);
    }

    
    //bool IsThisAnimal(Trash t) {
    //    return t.animal == this;
    //}

     public void TrashPicked() { 
        trashesLeft--;
        if (trashesLeft == 0) {
            GetComponent<AnimatorTimer>().enabled = false;
            GetComponent<Animator>().Play(happy_animation);
        }
     }



}
