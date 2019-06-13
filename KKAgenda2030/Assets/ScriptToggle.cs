using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptToggle : MonoBehaviour {

    public AnimatorTimer at;
    public float timer;

    public void ToggleInteractive() {
        at.enabled = true;
    }

    public void RestoreButton() {
        Invoke("ToggleInteractive", timer);
    }
}
