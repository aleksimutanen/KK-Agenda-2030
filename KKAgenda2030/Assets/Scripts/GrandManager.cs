using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandManager : MonoBehaviour {

    public static GrandManager instance;
    public bool paused;

    public GameObject mainMenuFolder;

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
        mainMenuFolder.SetActive(false);
        oceanGame.SetActive(true);
        oceanGameUI.gameObject.SetActive(true);
    }
}
