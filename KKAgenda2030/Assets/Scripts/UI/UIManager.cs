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

    public Image transitionBackGround;
    public Image transitionCircle;

    public Image trashGameTransition;

    public Image sliderImage;
    public Slider slider;
    
    public float fadeTime;

    bool paused;
    public bool transition;

    // TRASH GAME UI's //
    //=================//

    public Animator animator;

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
        transitionBackGround.GetComponent<Animator>().Play("OceanGameTransition");
    }

    public void OceanGameCircle() {
        transitionCircle.GetComponent<Animator>().Play("TransitionCircle");
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
        //GrandManager.instance.LaunchOceanGame();
        GrandManager.instance.StartCoroutine("LaunchOceanGame");
        //DisableMenuButtons();
    }

    public void LaunchTrashGame() {
        GrandManager.instance.StartCoroutine("LaunchTrashGame");
        //DisableMenuButtons();
    }

    public void LaunchBeeGame() {
        GrandManager.instance.LaunchBeeGame();
    }

    public void DisableMenuButtons() {
        foreach (Button b in menuButtons) b.gameObject.SetActive(false);
        foreach (Button b in menuButtons) b.interactable = false;
    }

    public void EnableMenuButtons() {
        foreach (Button b in menuButtons) b.gameObject.SetActive(true);
        foreach (Button b in menuButtons) b.interactable = true;
    }

    public void ReloadRunnerGame() {
        StartCoroutine("ReloadRunnerGameFade");
    }

    IEnumerator ReloadRunnerGameFade() {
        //moromoro
        RunnerGameManager.instance.RestartLevel();
        PauseButton();
        yield return null;
    }

    public void ReloadOceanGameLevel() {
        //OceanGameManager.instance.ReloadLevel();
        //PauseButton();
        StartCoroutine("ReloadGameFade");
    }

    IEnumerator ReloadGameFade() {
        transitionBackGround.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();

        yield return new WaitForSeconds(1f);

        OceanGameManager.instance.ReloadLevel();

        yield return new WaitForSeconds(1f);

        transitionBackGround.GetComponent<Animator>().Play("New State");
    }

    public void BackToMainMenu() {
        //OceanGameManager.instance.QuitToMenu();
        //GrandManager.instance.BackToMainMenu();
        //PauseButton();
        StartCoroutine("QuitGameFade");
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

    public void ReloadTrashGame() {
        //PauseButton();
        StartCoroutine("ReloadTrashGameFade");
    }

    IEnumerator ReloadTrashGameFade() {
        animator.Play("LevelSwitchNEW");
        PauseButton();

        //insert a proper time frame

        //  I
        //  I
        //  V

        yield return new WaitForSeconds(1f);

        //fade image on
        //yield return new WaitForSeconds(animation lenght);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;
    }

    public void TrashGameStartFade() {
        animator.Play("FadeOut");
    }

    IEnumerator QuitGameFade() {
        transitionBackGround.GetComponent<Animator>().Play("OceanGameQuickTransition");
        PauseButton();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenuForProjectScene");

        //OceanGameManager.instance.QuitToMenu();
        //GrandManager.instance.BackToMainMenu();

        //yield return new WaitForSeconds(1f);

        //transitionBackGround.GetComponent<Animator>().Play("New State");
    }

    public void NextPage() {
        pt.NextPage();
    }

    public void PreviousPage() {
        pt.PreviousPage();
    }

    // PAUSE UI ACTIONS //

    public void PauseButton() {
        //if (paused) {
        //    StartCoroutine(PauseAnim("PauseButtonAnimationOut"));
        //    paused = false;
        //} else {
        //    StartCoroutine(PauseAnim("PauseButtonAnimation"));
        //    paused = true;
        //}
        //print(paused);

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

            //pause.GetComponent<Animator>().Play(animName);

            //yield return new WaitForSeconds(1f);
            bool paused = GrandManager.instance.Pause();

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

    public void XD() {
        print("xd");
    }
}
