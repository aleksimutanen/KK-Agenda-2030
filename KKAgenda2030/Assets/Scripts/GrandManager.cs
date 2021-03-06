﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public enum SceneActive { Menu, Ocean }
public class GrandManager : MonoBehaviour {

    public static GrandManager instance;
    public SceneActive scene;
    public GameObject activeScene;
    public GameObject previousScene;

    public bool paused;

    public AudioMixer audioMixer;

    // Menu:
    public GameObject mainMenu;


    // Games:
    public GameObject oceanGame;
    public GameObject trashGame;

    // Menu UI's
    public GameObject optionsButton;
    public GameObject previousPageButton;
    public GameObject nextPageButton;
    public Slider oceanGameUI;

    // Fabric
    public string sharkMusic;
    public string stopMusic;
    public string ambient;
    public string stopAmbient;

    public GrandManager() {
    }

    void Start() {

        if (instance)
            Debug.LogError("2 GrandManagers found");
        instance = this;
        //Fabric.EventManager.Instance.PostEvent("ambient");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T))
            Time.timeScale = 1f;
    }

    public bool Pause() {
        if (!paused) {
            Time.timeScale = 0;
            paused = true;
            return true;
        } else {
            paused = false;
            Time.timeScale = 1;
            return false;
        }
    }

    public IEnumerator LaunchOceanGame() {
        //optionsButton.SetActive(false);
        //previousPageButton.SetActive(false);
        //nextPageButton.SetActive(false);

        var ui = FindObjectOfType<UIManager>();
        ui.fishGameTransition.GetComponent<Animator>().Play("OceanGameTransition");

        yield return new WaitForSeconds(1f);

        ui.fishTransitionCircle.gameObject.SetActive(true);
        ui.fishTransitionCircle.GetComponent<Animator>().Play("TransitionCircle");
        yield return new WaitForSeconds(3f);

        ui.fishTransitionCircle.gameObject.SetActive(false);

        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;

        SceneManager.LoadScene("FishGame");
        ////
        //mainMenu.SetActive(false);
        //oceanGame.SetActive(true);
        //oceanGameUI.gameObject.SetActive(true);
        //OceanGameManager.instance.StartGame();
        //activeScene = oceanGame;
        //scene = SceneActive.Ocean;
        ////

        //yield return new WaitForSeconds(1f);

        //ui.transitionBackGround.GetComponent<Animator>().Play("New State");

        //Fabric.EventManager.Instance.PostEvent("stopAmbient");
        //Fabric.EventManager.Instance.PostEvent("sharkMusic");

    }

    public IEnumerator LaunchTrashGame() {
        //optionsButton.SetActive(false);
        //previousPageButton.SetActive(false);
        //nextPageButton.SetActive(false);
        var ui = FindObjectOfType<UIManager>();
        ui.trashGameTransition.GetComponent<Animator>().Play("FakeLoading");
        yield return new WaitForSeconds(3.9f);
        //trashGame.SetActive(true);
        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public IEnumerator LaunchBeeGame() {
        var ui = FindObjectOfType<UIManager>();
        ui.runnerGameTransition.GetComponentInParent<Animator>().Play("RunnerLevelTransition");
        yield return new WaitForSeconds(3.5f);
        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;
        SceneManager.LoadScene("RunnerGame");
    }

    public IEnumerator LaunchMemoryGame() {
        var ui = FindObjectOfType<UIManager>();
        ui.memoryGameTransition.GetComponent<Animator>().Play("MemoryTransition");
        yield return new WaitForSeconds(3.5f);
        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;
        SceneManager.LoadScene("MemoryGame");
    }

    public IEnumerator LaunchWorldGame() {
        var ui = FindObjectOfType<UIManager>();
        ui.worldGameTransition.GetComponent<Animator>().Play("WorldGameTransition");
        yield return new WaitForSeconds(2.75f);
        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;
        SceneManager.LoadScene("WorldGame");
    }

    public IEnumerator LaunchClimateGame() {
        var ui = FindObjectOfType<UIManager>();
        ui.energyGameTransition.GetComponent<Animator>().Play("EnergyGameTransition");
        //gametransition for climategame here. Adjust also yield below
        yield return new WaitForSeconds(3f);
        FindObjectOfType<PersistentData>().pageIndex = FindObjectOfType<PageTurner>().pageIndex;
        SceneManager.LoadScene("Aleksi_devscene");
    }

    public void BackToMainMenu() {
        //previousScene = activeScene;
        //previousScene.SetActive(false);
        //activeScene = mainMenu;
        //activeScene.SetActive(true);
        //scene = SceneActive.Menu;
        SceneManager.LoadScene("MainMenu");

        //FindObjectOfType<UIManager>().EnableMenuButtons();
        //optionsButton.SetActive(true);
        //previousPageButton.SetActive(true);
        //nextPageButton.SetActive(true);

        Fabric.EventManager.Instance.PostEvent("stopMusic");
        Fabric.EventManager.Instance.PostEvent("ambient");
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("Music", volume);

    }

    public void SetSFXVolume(float volume) {
        audioMixer.SetFloat("Sfx", volume);
    }
}
