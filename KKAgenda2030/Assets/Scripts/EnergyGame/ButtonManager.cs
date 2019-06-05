using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public enum Scenes { livingRoom, Yard, Kitchen }

    public Scenes currentScene;

    public int sceneIndex;
    public List<SceneParent> sceneFolder;

    void Start() {
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextScene();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousScene();
    }

    public void NextScene() {
        if (sceneIndex == sceneFolder.Count - 1) return;

        foreach (Toggle toggle in sceneFolder[sceneIndex].toggles) {
            toggle.interactable = false;
        }
        //transition something

        sceneFolder[sceneIndex].gameObject.SetActive(false);
        sceneFolder[sceneIndex + 1].gameObject.SetActive(true);

        foreach (Toggle toggle in sceneFolder[sceneIndex + 1].toggles) {
            toggle.interactable = true;
        }

        sceneIndex++;
        currentScene = Scenes.livingRoom + sceneIndex;
    }

    public void PreviousScene() {
        if (sceneIndex == 0) return;

        foreach (Toggle toggle in sceneFolder[sceneIndex].toggles) {
            toggle.interactable = false;
        }
        //transition something

        sceneFolder[sceneIndex].gameObject.SetActive(false);
        sceneFolder[sceneIndex - 1].gameObject.SetActive(true);

        foreach (Toggle toggle in sceneFolder[sceneIndex - 1].toggles) {
            toggle.interactable = true;
        }

        sceneIndex--;
        currentScene = Scenes.livingRoom + sceneIndex;
    }
}
