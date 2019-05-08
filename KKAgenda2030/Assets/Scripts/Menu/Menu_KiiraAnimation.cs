using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_KiiraAnimation : MonoBehaviour { 

    public Animator childAnimator;

    public void ToMoveAnimation() {
        childAnimator.Play("MenuKiira_move");
    }

    public void ToIdleAnimation() {
        childAnimator.Play("MenuKiira_idle");
    }

    private void OnMouseDown() {
        if (gameObject.name == "KiiraBeeParent") {
            childAnimator.Play("MenuKiira_happy");
        }

    }
}
