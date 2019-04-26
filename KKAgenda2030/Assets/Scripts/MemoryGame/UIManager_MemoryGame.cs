using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MemoryGame : MonoBehaviour {

    private MemoryGameManager mgm;

    public GameObject SelfiePanel;
    public GameObject GamePanel;
    public GameObject DisabledObjectsFolder;

    public GameObject ShowPairsPanel;

    private void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
    }

    public void CloseSelfiePanel() {
        if(mgm._matches != 0) {
            StartCoroutine(CloseSelfie());
        } else {
            mgm.InsertPrefabValues();
            StartCoroutine(ShowAllPairs());
        }

    }

    IEnumerator CloseSelfie() {
        //SelfiePanel.GetComponent<Animator>().Play("Close_SelfiePanel");
        yield return new WaitForSeconds(2f);
        mgm.MatchCardPos.gameObject.transform.parent = DisabledObjectsFolder.transform;
        mgm.MatchCardPos2.gameObject.transform.parent = DisabledObjectsFolder.transform;
        SelfiePanel.SetActive(false);


    }

    IEnumerator ShowAllPairs() {
        print("näytä kaikki parit nyt");
        GamePanel.SetActive(false);
        SelfiePanel.SetActive(false);
        mgm.MatchCardPos.gameObject.transform.parent = DisabledObjectsFolder.transform;
        mgm.MatchCardPos2.gameObject.transform.parent = DisabledObjectsFolder.transform;
        yield return new WaitForSeconds(1f);
        ShowPairsPanel.SetActive(true);
        // TODO:
        // jos kuvanotto skipattu, niin näyttää oletustekstuuri(TODO) sen kuvan kohdalla 
    }

}
