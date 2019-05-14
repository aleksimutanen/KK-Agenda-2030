using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {

    public int trashesLeft;

    void Start() {
        trashesLeft = FindObjectsOfType<TrashBehaivor>().Count(t => t.animal == this);
    }

    
    //bool IsThisAnimal(Trash t) {
    //    return t.animal == this;
    //}



    void CheckTrashAmount() {
        // if all trashes are collected, play that animals animation

    }



}
