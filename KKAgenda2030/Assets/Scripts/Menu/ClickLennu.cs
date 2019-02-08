using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLennu : MonoBehaviour {

    public Animator animator;
    public string animationName;

    private void OnMouseDown() {
        animator.Play(animationName);
    }

}
