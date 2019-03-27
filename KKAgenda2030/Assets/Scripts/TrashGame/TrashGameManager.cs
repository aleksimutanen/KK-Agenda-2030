using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
    Game,
    Restart1,
    Restart2,
    GameMenu
};


public class TrashGameManager : MonoBehaviour {

    enum TimerType { Increase, Decrease };

    public static TrashGameManager instance = null;

    LevelChanger levelChanger;
    Scene activeScene;

    public Slider scoreSlider;
    public Slider endScoreSlider;
    public Slider totalScoreSlider;
    public AnimationCurve sliderAnimCurve;
    public Animator sliderAnimator;
    public Animator scoreMeterAnimator;

    public Image[] starImages;
    public float[] starScore;
    public Image[] totalStarImages;
    public float[] totalStarScore;
    public GameObject totalScoreBCG;

    public Text statusText;
    public GameObject trashCountObject;
    public GameObject trashAreaBCG;

    public GameState State;

    private int score = 0;
    public Spawner spwn;
    public GSpawners Gspwn;
    public bool levelCompleted = false;

    List<float> scoreTimers = new List<float>();
    List<TimerType> timerTypes = new List<TimerType>();

    PersistentData pd;
    public GameObject pdPrefab;


    //AUDIO
    public string netHit;


    void Awake() {
        //Check if instance already exists
        if (instance == null) {
            // print("TrashGameManager has been found");
        }

        //if not, set instance to this
        instance = this;
        // print("TrashGameManager is added to game");

        Time.timeScale = 1f;
    }

    void Start() {
        pd = FindObjectOfType<PersistentData>();
        if (pd == null) {
            var pdgo = Instantiate(pdPrefab);
            pd = pdgo.GetComponent<PersistentData>();
        }
        spwn = FindObjectOfType<Spawner>();
        Gspwn = FindObjectOfType<GSpawners>();
        levelChanger = FindObjectOfType<LevelChanger>();
        //FindObjectOfType<UIManager>().TrashGameStartFade();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            spwn.rubbish.Clear();
            spwn.rubbish.Add(new GameObject());
            spwn.Spawn();
            score = 100;
            scoreSlider.value = 100f;
        }


        for (int i = 0; i < scoreTimers.Count;) {
            scoreTimers[i] -= Time.deltaTime;
            if (scoreTimers[i] < 0) {
                scoreTimers.RemoveAt(i);
                timerTypes.RemoveAt(i);
            } else {
                if (timerTypes[i] == TimerType.Decrease) {
                    LoseScore(5f);
                } else if (timerTypes[i] == TimerType.Increase) {
                    GainScore(10f);
                }
                i++;
            }
        }
    }


    public IEnumerator LevelCompleted() {
        //SpriteAppear[] spriteAppears = FindObjectsOfType<SpriteAppear>();
        //for (int i = 0; i < spriteAppears.Length; i++) {
        //    spriteAppears[i].alphaFade();
        //}

        // If trashcan's idle after game complete is wanted, keep this in comments 
        //Gspwn.canFolder.gameObject.SetActive(false);

        trashCountObject.SetActive(false);
        trashAreaBCG.gameObject.SetActive(false);
        //statusText.text = "Taso suoritettu!";
        endScoreSlider.gameObject.SetActive(true);

        var ui = FindObjectOfType<UIManager>();

        yield return new WaitForSeconds(1f);
        sliderAnimator.Play("ScoreBarMove");
        yield return new WaitForSeconds(2f);

        float s = scoreSlider.value;
        float fillTime = 3f;

        float t = 0f;
        float fillSpeed = 1 / fillTime;

        while (t <= 1) {
            t += fillSpeed * Time.deltaTime;
            var curvedT = sliderAnimCurve.Evaluate(t);
            endScoreSlider.value = +(curvedT * s);
            for (int i = 0; i < starImages.Length; i++) {
                if (endScoreSlider.value >= starScore[i]) {
                    starImages[i].gameObject.SetActive(true);
                    ui.LevelEndStars2(starImages[i]);
                }
            }
            yield return null;
        }
        foreach (Image star in starImages)
            if (star.gameObject.activeSelf)
                pd.totalStarAmount += 1f;

        levelCompleted = false;
        yield return new WaitForSeconds(2f);
        scoreSlider.GetComponent<Animator>().Play("ScoreBarGroupFade");


        yield return new WaitForSeconds(2f);
        if (totalScoreSlider != null) {
            totalScoreBCG.SetActive(true);
            // jotain animaatioita ja juttuja ennen totalScore slideria + yield time
            if (pd.totalStarAmount > 6) {
                print("score yli 6 - paras");
                scoreMeterAnimator.Play("TotalScoreMeter_Good");
            }
            else if (pd.totalStarAmount > 3) {
                print("score yli 3 - ok");

                scoreMeterAnimator.Play("TotalScoreMeter_Ok");
            }
            else if (pd.totalStarAmount <= 3) {
                print("score alle 3 - huono");

                scoreMeterAnimator.Play("TotalScoreMeter_Bad");
            }
            yield return new WaitForSeconds(2f);

            totalScoreSlider.gameObject.SetActive(true);
            s = pd.totalStarAmount;
            fillTime = 3f;
            t = 0f;
            fillSpeed = 1 / fillTime;

            while (t <= 1) {
                t += fillSpeed * Time.deltaTime;
                var curvedT = sliderAnimCurve.Evaluate(t);
                totalScoreSlider.value = +(curvedT * s);
                for (int i = 0; i < totalStarImages.Length; i++) {
                    if (totalScoreSlider.value >= totalStarScore[i]) {
                        totalStarImages[i].gameObject.SetActive(true);
                        ui.LevelEndStars2(totalStarImages[i]);
                    }
                }
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        //// muu kuin lvl3
        //if (totalScoreSlider != null) {
        //    totalScoreSlider.gameObject.SetActive(true);
        //    s = pd.totalStarAmount;
        //    fillTime = 3f;
        //    t = 0f;
        //    fillSpeed = 1 / fillTime;

        //    while (t <= 1) {
        //        t += fillSpeed * Time.deltaTime;
        //        var curvedT = sliderAnimCurve.Evaluate(t);
        //        totalScoreSlider.value = +(curvedT * s);
        //        for (int i = 0; i < totalStarImages.Length; i++) {
        //            if (totalScoreSlider.value >= totalStarScore[i]) {
        //                totalStarImages[i].gameObject.SetActive(true);
        //                ui.LevelEndStars2(totalStarImages[i]);
        //            }
        //        }
        //        yield return null;
        //    }
        //    yield return new WaitForSeconds(1f);
        //}

        if (spwn.rubbish.Count == 0) {
            activeScene = SceneManager.GetActiveScene();
            levelCompleted = true;
            statusText.text = "";

            if (activeScene.name != "TrashGamelvl3") {
                levelChanger.StartCoroutine("LevelChange");
            } else { // vika lvl pelattu, palataan menuun
                levelChanger.StartCoroutine("LoadToMenu");
            }


            // OLD STYLE!
            // Vaihdetaan scene seuraavaan tasoon fadejen kanssa
            //if (SceneManager.GetActiveScene().buildIndex != 3) {
            //    levelChanger.FadeToNextLevel();
            //} else { // vikan levelin jälkeen loadi vaikka menuun? Scenenro?
            //    yield return new WaitForSeconds(5f);
            //    // jotain fancya UI-animaatioita tähän ennen menuun paluuta?
            //    SceneManager.LoadScene(0);
            //}

        }
    }

    public void AddedPoints() {
        //Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Increase);
    }

    void GainScore(float amount) {
        scoreSlider.GetComponent<Animator>().Play("GreenFlash");
        scoreSlider.value += amount * Time.deltaTime;
    }

    void LoseScore(float amount) {
        scoreSlider.GetComponent<Animator>().Play("RedFlash");
        scoreSlider.value -= amount * Time.deltaTime;
    }

    public void DeletingPoints() {
        //Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Decrease);
    }

    public void ResSpawning() {
        spwn.Spawn();
    }
}