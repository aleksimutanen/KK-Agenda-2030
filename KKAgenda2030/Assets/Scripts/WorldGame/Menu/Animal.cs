using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {

    public int trashesLeft;
    public string happy_animation;
    public GameObject lightsFolder;


    void Start() {
        FindTrashes();
    }

    
    //bool IsThisAnimal(Trash t) {
    //    return t.animal == this;
    //}

    public void FindTrashes() {
        trashesLeft = FindObjectsOfType<TrashBehaivor>().Count(t => t.animal == this);
        GetComponent<AnimatorTimer>().enabled = true;

    }

    public void TrashPicked() { 
        trashesLeft--;
        if (trashesLeft == 0) {
            GetComponent<AnimatorTimer>().enabled = false;
            GetComponent<Animator>().Play(happy_animation);
            lightsFolder.SetActive(true);
        }
     }



}
