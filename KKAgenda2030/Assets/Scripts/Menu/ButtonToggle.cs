using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    public Button button;
    public float timer;

    public void ToggleInteractive() {
        button.interactable = true;
    }

    public void RestoreButton() {
        Invoke("ToggleInteractive", timer);
    }


}
