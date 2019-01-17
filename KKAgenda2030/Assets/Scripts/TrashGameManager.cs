using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
           { Game,
             Restart1,
             Restart2,
             GameMenu };


public class TrashGameManager : MonoBehaviour {

    enum TimerType { Increase, Decrease };

    public static TrashGameManager instance = null;

    public Slider scoreSlider;
    public Slider endScoreSlider;
    public Slider totalScoreSlider;
    public AnimationCurve sliderAnimCurve;
    public Animator sliderAnimator;

    public Image[] starImages;
    public float[] starScore;
    public Image[] totalStarImages;
    public float[] totalStarScore;



    public Text statusText;
    public GameState State;
   
    private int score = 0;
    public Spawner spwn;
    public GSpawners Gspwn;
    public bool levelCompleted = false;

    List<float> scoreTimers = new List<float>();
    List<TimerType> timerTypes = new List<TimerType>();

    PersistentData pd;
    public GameObject pdPrefab;
    
   

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
           // print("TrashGameManager has been found");
        }
        
        //if not, set instance to this
            instance = this;
           // print("TrashGameManager is added to game");
    }

    void Start()
    {

        pd = GameObject.FindObjectOfType<PersistentData>();
        if (pd == null) {
            var pdgo = Instantiate(pdPrefab);
            pd = pdgo.GetComponent<PersistentData>();
        }

        CheckCurrentActiveSceneState();

        spwn = FindObjectOfType<Spawner>();
        Gspwn = FindObjectOfType<GSpawners>();
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
                if (timerTypes[i] == TimerType.Decrease)
                    LoseScore(5f);
                else if (timerTypes[i] == TimerType.Increase)
                    GainScore(10f);
                i++;
            }
        }
    }

    public IEnumerator LevelCompleted() {

        statusText.text = "Taso suoritettu!";
        endScoreSlider.gameObject.SetActive(true);

        var ui = FindObjectOfType<UIManager>();

        sliderAnimator.Play("ScoreBarMove");
        yield return new WaitForSeconds(3f);

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
                    ui.LevelEndStars(starImages[i]);
                }
            }
            yield return null;
        }
        Debug.LogError("FOO!!!!!");
        foreach (Image star in starImages)
            if (star.gameObject.activeSelf)
                pd.totalStarAmount += 1f;

        // feidaus pois ja keskelle takasin vaakassa.

        levelCompleted = false;

        if (spwn.rubbish.Count == 0)
        {
            levelCompleted = true;
                       
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

            // print(levelCompleted);
        }

        // jotain animaatioita ja juttuja ennen totalScore slideria + yield time

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
                    ui.LevelEndStars(totalStarImages[i]);
                }
            }
            yield return null;
        }


    }


    public void UpdatePoints()
    {
        //scoreText.text = " " + score;
    }

    public void AddedPoints()
    {
        //Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Increase);
        //score += 25;
    }

    void GainScore(float amount) {
        scoreSlider.value += amount * Time.deltaTime;
    }

    public void DeletingPoints()
    {
        //score -= 30;
        //Mathf.RoundToInt(scoreSlider.value);
        scoreTimers.Add(1f);
        timerTypes.Add(TimerType.Decrease);
    }

    void LoseScore(float amount) {
        scoreSlider.value -= amount * Time.deltaTime;
    }

    public void ResSpawning()
    {
        spwn.Spawn();
    }


    private void CheckCurrentActiveSceneState()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        
           
        if (State == GameState.Game)
        {
            currentSceneName = "Joni_devscene";
        }

        if (State == GameState.Restart1)
        {
            currentSceneName = "Level-2";
           
        }


        if (State == GameState.Restart2)
        {
            currentSceneName = "Joni_devscene";
            
        }

        // print(currentSceneName);
    }

    
}