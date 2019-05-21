using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    PageTurner pt;

    public GameObject bookCover;
    public Button pause;
    public Button[] menuButtons;
    public Button[] pauseMenuButtons;
    public Image[] pauseMenuImageList;

    public Image fishGameTransition;
    public Image fishTransitionCircle;

    public Image trashGameTransition;

    public Image runnerGameTransition;

    public Image memoryGameTransition;

    public Image worldGameTransition;


    public Image sliderImage;
    public Slider slider;
    
    public float fadeTime;

    bool paused;
    public bool transition;

    // TRASH GAME UI's //
    //=================//

    public Animator animator;
    public MenuGameManager MGM;

	void Start () {
        Time.timeScale = 1f;
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
        fishGameTransition.GetComponent<Animator>().Play("OceanGameTransition");
    }

    public void OceanGameCircle() {
        fishTransitionCircle.GetComponent<Animator>().Play("TransitionCircle");
    }

    public void LevelEndStars(Image star) {
        star.GetComponent<Animator>().Play("StarPop");
    }


    public void LevelEndStars2(Image star) {
        star.GetComponent<Animator>().Play("StarPop_ThrashGame");
    }

    public void DisableStar(Image star) {
        star.GetComponent<Animator>().Play("New State");
    }


    // GAME LAUNCH FUNCTIONS //
    public void LaunchOceanGame() {
        GrandManager.instance.StartCoroutine("LaunchOceanGame");
    }

    public void LaunchTrashGame() {
        GrandManager.instance.StartCoroutine("LaunchTrashGame");
        //DisableMenuButtons();
    }

    public void LaunchBeeGame() {
        GrandManager.instance.StartCoroutine("LaunchBeeGame");

    }
    public void LaunchMemoryGame() {
        GrandManager.instance.StartCoroutine("LaunchMemoryGame");

    }
    public void LaunchWorldGame() {
        GrandManager.instance.StartCoroutine("LaunchWorldGame");

    }

    public void DisableMenuButtons() {
        foreach (Button b in menuButtons) b.gameObject.SetActive(false);
        foreach (Button b in menuButtons) b.interactable = false;
    }

    public void EnableMenuButtons() {
        foreach (Button b in menuButtons) b.gameObject.SetActive(true);
        foreach (Button b in menuButtons) b.interactable = true;
    }


    ///////// 
    
    public void ReloadRunnerGame() {
        StartCoroutine("ReloadRunnerGameFade");
    }

    IEnumerator ReloadRunnerGameFade() {
        runnerGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        //if (!GrandManager.instance.paused) 
        PauseButton();
        yield return new WaitForSeconds(1f);
        RunnerGameManager.instance.RestartLevel();
    }

    public void RestartRunnerGame() {
        StartCoroutine("RestartRunnerGameFade");
    }

    IEnumerator RestartRunnerGameFade() {
        //Time.timeScale = 1f;
        runnerGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        yield return new WaitForSeconds(1f);
        RunnerGameManager.instance.RestartLevel();
    }

    public void ReloadOceanGame() {
        StartCoroutine("ReloadOceanGameFade");
    }

    IEnumerator ReloadOceanGameFade() {
        fishGameTransition.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        OceanGameManager.instance.ReloadLevel();
        yield return new WaitForSeconds(1f);
        fishGameTransition.GetComponent<Animator>().Play("New State");
    }

    public void ReloadTrashGame() {
        StartCoroutine("ReloadTrashGameFade");
    }

    IEnumerator ReloadTrashGameFade() {
        animator.Play("LevelSwitchNEW");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReloadMemoryGame() {
        StartCoroutine("ReloadMemoryGameFade");
    }

    IEnumerator ReloadMemoryGameFade() {
        memoryGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadWorldGame() {
        StartCoroutine("ReloadWorldGameFade");
    }

    IEnumerator ReloadWorldGameFade() {
        worldGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    ///////// 


    public void QuitOceanGame() {
        StartCoroutine("QuitOceanGameFade");
    }

    IEnumerator QuitOceanGameFade() {
        fishGameTransition.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        // palaa roskispelin menuun
        StartCoroutine("QuitTrashGameFade");
    }

    IEnumerator QuitTrashGameFade() {
        animator.Play("LevelSwitchNEW");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public void QuitRunnerGame() {
        StartCoroutine("QuitRunnerGameFade");      
    }

    IEnumerator QuitRunnerGameFade() {
        runnerGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
    public void QuitMemoryGame() {
        StartCoroutine("QuitMemoryGameFade");
    }

    IEnumerator QuitMemoryGameFade() {
        memoryGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public void QuitWorldGame() {
        StartCoroutine("QuitWorldGameFade");
    }

    IEnumerator QuitWorldGameFade() {
        worldGameTransition.GetComponent<Animator>().Play("RunnerGameQuickTransition");
        PauseButton();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }



    public void TrashGameStartFade() {
        animator.Play("FadeOut");
    }

    public void ReplayMinigame() {
        MGM.ResetMiniGame();
    }
   
    public void NextPage() {
        pt.NextPage();
    }

    public void PreviousPage() {
        pt.PreviousPage();
    }

    // PAUSE UI ACTIONS //

    public void PauseButton() {
        StartCoroutine("PauseAnim");
    }

    IEnumerator PauseAnim() {
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

            GrandManager.instance.Pause();

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

}
