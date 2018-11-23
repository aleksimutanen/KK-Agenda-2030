﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    PageTurner pt;

    public GameObject bookCover;
    public Button pause;
    public Button[] pauseMenuButtons;

    public Image sliderImage;
    public Slider slider;

    bool paused;
    public bool transition;

	void Start () {
        pt = FindObjectOfType<PageTurner>();
        for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = false;
    }

    public void HitAvoidable() {
        sliderImage.GetComponent<Animator>().Play("OceanGameScoreBar");
    }

    public void HitFood() {
        sliderImage.GetComponent<Animator>().Play("OceanGameScoreBarIncrease");
    }

    public void OceanGameLevelComplete() {
        slider.GetComponent<Animator>().Play("OceanGameLevelEnd");
    }

    public void LaunchOceanGame() {
        GrandManager.instance.LaunchOceanGame();
    }

    public void NextPage() {
        pt.NextPage();
    }

    public void PreviousPage() {
        pt.PreviousPage();
    }

    public void PauseButton() {
        if (paused) {
            StartCoroutine(PauseAnim("PauseButtonAnimationOut"));
            paused = false;
        } else {
            StartCoroutine(PauseAnim("PauseButtonAnimation"));
            paused = true;
        }
    }

    IEnumerator PauseAnim(string animName) {
        if (GrandManager.instance.paused) {
            GrandManager.instance.Pause();
            transition = true;
            for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = false;
            pause.GetComponent<Animator>().Play(animName);
            transition = false;
            yield return null;
        } else {
            transition = true;
            pause.GetComponent<Animator>().Play(animName);
            yield return new WaitForSeconds(1f);
            bool paused = GrandManager.instance.Pause();
            for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = true;
            transition = false;
            yield return null;
        }
    }
}
