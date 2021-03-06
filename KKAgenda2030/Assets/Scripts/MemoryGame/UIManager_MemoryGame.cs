﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MemoryGame : MonoBehaviour {

    private MemoryGameManager mgm;

    public GameObject AvatarChoosePanel;
    public GameObject SelfiePanel;
    public GameObject GamePanel;
    public GameObject CardsFolder;
    public GameObject DisabledObjectsFolder;
    public GameObject ShowPairsPanel;
    public Button CameraButton;

    public AudioSource memorySound;
    public AudioClip winSound;
    public AudioClip exitButton;

    private void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
    }

    // CONTINUE BUTTON/START GAME INTERACTION
    // when player pictures are choosed, start coroutine from button
    public void StartPlaying() {
        StartCoroutine(StartGame());
    }


    IEnumerator StartGame() {
        AvatarChoosePanel.SetActive(false);
        // Avatar images off and back on with delay?
        mgm.SlotSelected(0);
        foreach (var item in mgm.playerAvatars) {
            var avatar = item.transform;
            var buttonParent = avatar.Find("Parent");
            buttonParent.GetComponentInChildren<Button>().interactable = false;
        }
        yield return new WaitForSeconds(1f);
        CardsFolder.SetActive(true);
        mgm.InitializeCards();
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
        memorySound.PlayOneShot(exitButton, 1.2f);
        yield return new WaitForSeconds(1.5f);
        //SelfiePanel.GetComponent<Animator>().Play("Close_SelfiePanel");
        //yield return new WaitForSeconds(1f);
        mgm.MatchCardPos.gameObject.transform.parent = DisabledObjectsFolder.transform;
        mgm.MatchCardPos2.gameObject.transform.parent = DisabledObjectsFolder.transform;
        mgm.emotions[mgm.lastFoundPairValue].SetActive(false);
        SelfiePanel.SetActive(false);


    }

    IEnumerator ShowAllPairs() {
        print("näytä kaikki parit nyt");
        memorySound.PlayOneShot(winSound);
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
