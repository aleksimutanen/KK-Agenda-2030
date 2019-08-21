using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuLoad : MonoBehaviour {

    void Start() {
        StartCoroutine(LoadMenu());

    }

    IEnumerator LoadMenu() {
        AsyncOperation AO = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f) {
            yield return null;
        }

        AO.allowSceneActivation = true;
        //SceneManager.LoadScene(1);
    }

}
