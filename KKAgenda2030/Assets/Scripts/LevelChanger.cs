using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

    int maxBuildIndex = 2;
    public Animator animator;
    private int levelToLoad;
    TrashGameManager TGM;
    public Image fakeLoad;

    private void Awake() {
        TGM = FindObjectOfType<TrashGameManager>();
    }

    void Update() {
        // for testing
        //if (Input.GetMouseButtonDown(0)) {
        //    FadeToNextLevel();
        //}
    }

    public void FadeToNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == maxBuildIndex)
            // Menuscenen nro tähän
            SceneManager.LoadScene(0);
        else
            // lataa seuraava buildissa oleva lvl
            FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void FadeToLevel(int levelIndex) {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        TGM.endScoreSlider.gameObject.SetActive(false);
        fakeLoad.gameObject.SetActive(true);
        fakeLoad.GetComponent<Animator>().Play("FakeLoading");


    }

    public void OnFadeComplete() {
        if (SceneManager.GetActiveScene().buildIndex == maxBuildIndex)
            SceneManager.GetSceneByBuildIndex(0);
        else
            SceneManager.LoadScene(levelToLoad);
    }
}
