﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    PageTurner pt;

    public GameObject bookCover;
    public Button pause;
    public Button[] pauseMenuButtons;
    public Image[] pauseMenuImageList;

    public Image transitionBackGround;
    public Image transitionCircle;
    public Image sliderImage;
    public Slider slider;
    
    public float fadeTime;

    bool paused;
    public bool transition;

	void Start () {
        pt = FindObjectOfType<PageTurner>();
        for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) LaunchOceanGame();
        if (Input.GetKeyDown(KeyCode.U)) GrandManager.instance.BackToMainMenu();
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

    public void OceanGameBackGroundTransition() {
        transitionBackGround.GetComponent<Animator>().Play("OceanGameTransition");
    }

    public void OceanGameCircle() {
        transitionCircle.GetComponent<Animator>().Play("TransitionCircle");
    }

    public void LevelEndStars(Image star) {
        star.GetComponent<Animator>().Play("StarPop");
    }

    public void LaunchOceanGame() {
        //GrandManager.instance.LaunchOceanGame();
        GrandManager.instance.StartCoroutine("LaunchOceanGame");
    }

    public void ReloadOceanGameLevel() {
        //OceanGameManager.instance.ReloadLevel();
        //PauseButton();
        StartCoroutine("ReloadGameFade");
    }

    IEnumerator ReloadGameFade() {
        transitionBackGround.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();

        yield return new WaitForSeconds(1f);

        OceanGameManager.instance.ReloadLevel();

        yield return new WaitForSeconds(1f);

        transitionBackGround.GetComponent<Animator>().Play("New State");
    }

    public void BackToMainMenu() {
        //OceanGameManager.instance.QuitToMenu();
        //GrandManager.instance.BackToMainMenu();
        //PauseButton();
        StartCoroutine("QuitGameFade");
    }

    IEnumerator QuitGameFade() {
        transitionBackGround.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();

        yield return new WaitForSeconds(1f);

        OceanGameManager.instance.QuitToMenu();
        GrandManager.instance.BackToMainMenu();

        yield return new WaitForSeconds(1f);

        transitionBackGround.GetComponent<Animator>().Play("New State");
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
        print(paused);
    }

    IEnumerator PauseAnim(string animName) {
        if (GrandManager.instance.paused) {
            //unpaused
            transition = true;

            for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = false;

            Color[] b = new Color[pauseMenuImageList.Length];
            float[] d = new float[pauseMenuImageList.Length];

            for (int i = 0; i < b.Length; i++) {
                b[i] = pauseMenuImageList[i].color;
                d[i] = b[i].a;
            }

            while (d[0] >= 0) {
                for (int i = 0; i < pauseMenuImageList.Length; i++) {
                    d[i] -= Time.unscaledDeltaTime * (1 / fadeTime);
                    b[i].a = d[i];
                    pauseMenuImageList[i].color = b[i];
                }
                yield return null;
            }

            GrandManager.instance.Pause();

            transition = false;
        } else {
            //paused
            transition = true;

            //pause.GetComponent<Animator>().Play(animName);

            //yield return new WaitForSeconds(1f);
            bool paused = GrandManager.instance.Pause();

            Color[] b = new Color[pauseMenuImageList.Length];
            float[] d = new float[pauseMenuImageList.Length];

            for (int i = 0; i < b.Length; i++) {
                b[i] = pauseMenuImageList[i].color;
                d[i] = b[i].a;
            }

            while (d[0] <= 1) {
                for (int i = 0; i < pauseMenuImageList.Length; i++) {
                    d[i] += Time.unscaledDeltaTime * (1 / fadeTime);
                    b[i].a = d[i];
                    pauseMenuImageList[i].color = b[i];
                }
                yield return null;
            }
            for (int i = 0; i < pauseMenuButtons.Length; i++) pauseMenuButtons[i].interactable = true;

            transition = false;
        }
    }

    public void XD() {
        print("xd");
    }
}
