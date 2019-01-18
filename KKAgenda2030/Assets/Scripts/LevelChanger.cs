using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    int maxBuildIndex = 2;
    public Animator animator;
    private int levelToLoad;

    void Update() {
        // for testing
        //if (Input.GetMouseButtonDown(0)) {
        //    FadeToNextLevel();
        //}
    }

    public void FadeToNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == maxBuildIndex)
            // palaa menuun?
            SceneManager.LoadScene(0);
        else
            // lataa seuraava buildissa oleva lvl
            FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex) {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        if (SceneManager.GetActiveScene().buildIndex == maxBuildIndex)
            SceneManager.GetSceneByBuildIndex(0);
        else
            SceneManager.LoadScene(levelToLoad);
    }
}
