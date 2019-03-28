using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    public string sceneName;

    public void LoadToMenu() {
        SceneManager.LoadScene(0);
    }

    public void SwitchScene() {
        SceneManager.LoadScene(sceneName);
    }
}
