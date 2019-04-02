using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarClickBehaviour : MonoBehaviour {

    MemoryGameManager mgm;
    public int thisPicIndx;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
    }


    public void ClickAvatar() {
        print("image hit");
        var currentPicIndx = mgm.pictureIndx;
        if (currentPicIndx != thisPicIndx) {
            mgm.pictureIndx = thisPicIndx;
            // toggle some effect?
        }
    }
}
