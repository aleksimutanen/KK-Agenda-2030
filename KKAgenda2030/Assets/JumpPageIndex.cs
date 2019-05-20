using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPageIndex : MonoBehaviour {

    public int thisPageIndex;
    PageTurner pt;

    private void Start() {
        pt = GameObject.Find("PageTurner").GetComponent<PageTurner>();
    }

    // indexpage button click
    public void JumpToPage() {
        pt.JumpPageFromIndex(thisPageIndex);
    }


}
