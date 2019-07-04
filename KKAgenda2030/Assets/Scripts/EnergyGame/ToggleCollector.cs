using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCollector : MonoBehaviour
{

    public int totalTogglesRight;
    public Image emptyImg;
    public Text finishText;

    public void SwitchRight() {
        totalTogglesRight++;
        if (totalTogglesRight == FindObjectOfType<ButtonManager>().sceneFolder.Count * 3) {
            print("moro");
            StartCoroutine("QuitTransition");
        }
    }

    IEnumerator QuitTransition() {
        emptyImg.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        finishText.GetComponent<Animator>().Play("TextTransition");
        yield return new WaitForSeconds(5f);
        FindObjectOfType<UIManager>().FinishEnergyGame();
    }

    public void SwitchWrong() {
        totalTogglesRight--;
    }
}
