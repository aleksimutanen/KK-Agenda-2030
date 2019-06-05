using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public enum Scenes { livingRoom, Yard, Kitchen }

    public Scenes currentScene;

    public int sceneIndex;
    public List<SceneParent> sceneFolder;

    public Image fadeImage;
    Animator imgAnim;

    void Start() {
        imgAnim = fadeImage.GetComponent<Animator>();
        imgAnim.Play("FadeOut");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextScene();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousScene();
    }

    public void NextScene() {
        if (sceneIndex == sceneFolder.Count - 1 || GrandManager.instance.paused) return;
        sceneIndex++;
        StartCoroutine("NextSceneTransition");
    }

    IEnumerator NextSceneTransition() {
        foreach (Toggle toggle in sceneFolder[sceneIndex - 1].toggles) {
            toggle.interactable = false;
        }

        foreach (Button button in sceneFolder[sceneIndex - 1].buttons) {
            button.interactable = false;
        }

        imgAnim.Play("RunnerGameQuickTransition");
        yield return new WaitForSeconds(1f);

        sceneFolder[sceneIndex - 1].gameObject.SetActive(false);
        sceneFolder[sceneIndex].gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        foreach (Toggle toggle in sceneFolder[sceneIndex].toggles) {
            toggle.interactable = true;
        }

        foreach(Button button in sceneFolder[sceneIndex].buttons) {
            button.interactable = true;
        }

        //sceneIndex++;
        currentScene = Scenes.livingRoom + sceneIndex;
    }

    public void PreviousScene() {
        if (sceneIndex == 0 || GrandManager.instance.paused) return;
        sceneIndex--;
        StartCoroutine("PreviousSceneTransition");
    }

    IEnumerator PreviousSceneTransition() {
        foreach (Toggle toggle in sceneFolder[sceneIndex + 1].toggles) {
            toggle.interactable = false;
        }

        foreach (Button button in sceneFolder[sceneIndex + 1].buttons) {
            button.interactable = false;
        }

        imgAnim.Play("RunnerGameQuickTransition");
        yield return new WaitForSeconds(1f);

        sceneFolder[sceneIndex + 1].gameObject.SetActive(false);
        sceneFolder[sceneIndex].gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        foreach (Toggle toggle in sceneFolder[sceneIndex].toggles) {
            toggle.interactable = true;
        }

        foreach (Button button in sceneFolder[sceneIndex].buttons) {
            button.interactable = true;
        }

        //sceneIndex--;
        currentScene = Scenes.livingRoom + sceneIndex;
    }
}
