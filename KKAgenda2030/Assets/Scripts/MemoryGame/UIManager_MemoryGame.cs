using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MemoryGame : MonoBehaviour {

    private MemoryGameManager mgm;

    public GameObject SelfiePanel;

    private void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
    }

    public void CloseSelfiePanel() {
        StartCoroutine(CloseSelfie());
    }

    IEnumerator CloseSelfie() {
        yield return new WaitForSeconds(2f);
        SelfiePanel.GetComponent<Animator>().Play("Close_SelfiePanel");
        yield return new WaitForSeconds(.5f);
        SelfiePanel.SetActive(false);
    }


}
