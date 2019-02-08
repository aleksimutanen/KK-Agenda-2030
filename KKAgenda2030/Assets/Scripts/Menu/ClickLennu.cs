using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLennu : MonoBehaviour {

    public Animator animator;

    private void OnMouseDown() {
        animator.Play("Lennu_show");
    }

}
