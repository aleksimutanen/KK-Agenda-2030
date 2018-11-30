using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneActive { Menu, Ocean }
public class GrandManager : MonoBehaviour {

    public static GrandManager instance;
    public SceneActive scene;
    public GameObject activeScene;
    public GameObject previousScene;

    public bool paused;

    public GameObject mainMenu;

    public GameObject oceanGame;
    public Slider oceanGameUI;

	void Start () {
        if (instance)
            Debug.LogError("2 GrandManagers found");
        instance = this;
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            Pause();
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

    public void LaunchOceanGame() {
        mainMenu.SetActive(false);
        oceanGame.SetActive(true);
        oceanGameUI.gameObject.SetActive(true);
        OceanGameManager.instance.StartGame();
        activeScene = oceanGame;
        scene = SceneActive.Ocean;
    }

    public void BackToMainMenu() {
        previousScene = activeScene;
        previousScene.SetActive(false);
        activeScene = mainMenu;
        activeScene.SetActive(true);
        scene = SceneActive.Menu;
    }
}
