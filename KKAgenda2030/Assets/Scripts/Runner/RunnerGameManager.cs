using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class RunnerGameManager : MonoBehaviour {

    public static RunnerGameManager instance;

    public enum TimerType { GainSmall, GainBig, LoseSmall, LoseBig }


    // Game logic
    [SerializeField] int levelIndex;

    [SerializeField] List<TimerType> timerTypes = new List<TimerType>();
    [SerializeField] List<float> scoreTimers = new List<float>();

    [Range(0.5f, 5f)] public float uiScoreSliderAnimationTime;

    [SerializeField] GameObject[] levelPrefabs;
    [SerializeField] GameObject[] levels;
    [SerializeField] GameObject levelFolder;

    [SerializeField] float gainSmallScore;
    [SerializeField] float gainBigScore;

    [SerializeField] float loseSmallScore;
    [SerializeField] float loseBigScore;

    bool invulnerable;

    public int livesLeft;
    int maxLives;
    [SerializeField] int foodCollected;

    //

    // UI
    [SerializeField] Slider scoreSlider;
    [SerializeField] Image sliderFill;
    [SerializeField] Text livesLeftText;
    [SerializeField] List<Image> lifeImages;
    public List<Image> memList;

    [SerializeField] AnimationCurve fillColor;
    [SerializeField] Color sliderFillColor;
    [SerializeField] Color hitAvoidableColor;
    [SerializeField] Color hitCollectableColor;

    [SerializeField] Image fadeImage;

    public GameObject GameoverPanel;

    //

    RunnerController character;
    Vector3 charStartPos;

    void Start() {
        if (instance)
            Debug.LogError("2+ RunnerManagers found!");

        fadeImage.GetComponent<Animator>().Play("FadeOut");
        memList = lifeImages;
        instance = this;
        maxLives = livesLeft;
        character = FindObjectOfType<RunnerController>();
        charStartPos = character.transform.position;
        levelIndex = 0;
        LaunchGame();
    }

    void LaunchGame() {
        GameObject go = Instantiate(levelPrefabs[levelIndex], Vector3.zero, transform.rotation);
        go.transform.parent = levelFolder.transform;
        levels[levelIndex] = go;
    }

    void Update() {
        // for testing only
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0f;
            GameoverPanel.SetActive(true);
        }


        for (int i = 0; i < scoreTimers.Count;) {
            scoreTimers[i] -= Time.deltaTime;
            if (scoreTimers[i] < 0) {
                scoreTimers.RemoveAt(i);
                timerTypes.RemoveAt(i);
            } else {
                if (timerTypes[i] == TimerType.LoseSmall)
                    LoseScore(loseSmallScore / uiScoreSliderAnimationTime);
                else if (timerTypes[i] == TimerType.LoseBig)
                    LoseScore(loseBigScore / uiScoreSliderAnimationTime);
                else if (timerTypes[i] == TimerType.GainSmall)
                    GainScore(gainSmallScore / uiScoreSliderAnimationTime);
                else if (timerTypes[i] == TimerType.GainBig)
                    GainScore(gainBigScore / uiScoreSliderAnimationTime);
                i++;
            }
        }
    }

    public void HitFlower() {
        foodCollected++;
        if (foodCollected == 10) {
            //NextLevel();
            //blah blah
        }
    }

    public void HitCollectable(TimerType type) {
        if (type == TimerType.LoseBig || type == TimerType.LoseSmall) {
            Debug.LogError("Wrong type used in collectable");
            return;
        }
        scoreTimers.Add(uiScoreSliderAnimationTime);
        timerTypes.Add(type);
        StartCoroutine(HitGameObject(sliderFillColor, hitCollectableColor));
    }

    public void HitAvoidable(TimerType type) {
        if (type == TimerType.GainBig || type == TimerType.GainSmall) {
            Debug.LogError("Wrong type used in avoidable");
            return;
        }
        scoreTimers.Add(uiScoreSliderAnimationTime);
        timerTypes.Add(type);
        StartCoroutine(HitGameObject(sliderFillColor, hitAvoidableColor));
    }

    void GainScore(float amount) {
        scoreSlider.value += amount * Time.deltaTime;
    }

    void LoseScore(float amount) {
        scoreSlider.value -= amount * Time.deltaTime;
    }

    public IEnumerator HitGameObject(Color currentColor, Color newColor) {
        //modify scoreslider

        float fillTime = uiScoreSliderAnimationTime;
        float t = 0f;
        float fillSpeed = 1 / fillTime;

        while (t <= 1) {
            t += fillSpeed * Time.deltaTime;
            var curvedT = fillColor.Evaluate(t);
            //print(curvedT);
            sliderFill.color = Color.Lerp(currentColor, newColor, curvedT);
            yield return null;
            //foreach (SpriteRenderer sr in sharkSprites) sr.color = Color.Lerp(Color.white, ateTrashColor, curvedT);
        }

        sliderFill.color = Color.white;
    }

    public void NextLevel() {
        levelIndex++;
        Destroy(levels[levelIndex - 1]);
        levels[levelIndex].SetActive(true);
        scoreSlider.value = 0f;
    }

    public void RestartLevel() {
        //destroy and spawn map for current level
        GameObject levelClone = Instantiate(levelPrefabs[levelIndex], Vector3.zero, transform.rotation);
        Destroy(levels[levelIndex]);
        levels[levelIndex] = levelClone;
        levels[levelIndex].transform.parent = levelFolder.transform;

        //reset lives, UI and character position
        livesLeft = maxLives;
        //livesLeftText.text = livesLeft + " / " + maxLives;
        scoreSlider.value = 0f;
        foodCollected = 0;

        foreach (Image i in memList) i.gameObject.SetActive(true);
        lifeImages = memList;

        //character.transform.position = charStartPos;
        //character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        character.ResetCharacter();
    }

    public void LoseLife() {
        if (invulnerable) {
            return;
        }

        for (int i = 0; i < lifeImages.Count; i++) {
            if (lifeImages[i].IsActive()) {
                lifeImages[i].gameObject.SetActive(false);
                break;
            }
        }

        HitAvoidable(TimerType.LoseSmall);
        invulnerable = true;
        livesLeft--;
        //livesLeftText.text = livesLeft + " / " + maxLives;
        StartCoroutine("VignetteFlash");
        StartCoroutine("SpriteFlash");
        if (livesLeft == 0) {
            Time.timeScale = 0f;
            GameoverPanel.SetActive(true);
            //StartCoroutine("LevelTransition");
        }
    }

    IEnumerator LevelTransition() {
        FindObjectOfType<Map>().tfSpeed = 0f;
        yield return new WaitForSeconds(2f);
        RestartLevel();
        FindObjectOfType<Map>().tfSpeed = 1.5f;
    }

    IEnumerator VignetteFlash() {
        var ppVolume = FindObjectOfType<PostProcessVolume>();
        var t0 = Time.realtimeSinceStartup;
        var tEnd = 2f;
        var timer = 0f;

        while (timer < tEnd) {
            timer = Time.realtimeSinceStartup - t0;
            ppVolume.weight = Mathf.Sin(2 * Mathf.PI / (2 * tEnd) * timer);
            //ppVolume.weight = Mathf.PingPong(timer, 1);

            yield return null;
        }
        ppVolume.weight = 0;
    }

    IEnumerator SpriteFlash() {
        var spriteR = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>();
        var t0 = Time.realtimeSinceStartup;
        var timer = 0f;
        var waveLength = 1f;

        int flashes = 2;

        while (timer < flashes * waveLength) {
            timer = Time.realtimeSinceStartup - t0;
            var alphaSin = 1 - (Mathf.Sin(3 / 2 * Mathf.PI + 2 * Mathf.PI / waveLength * timer) * 0.5f + 0.5f);
            spriteR.color = new Color(1, 1, 1, alphaSin);
            yield return null;
        }

        spriteR.color = Color.white;
        invulnerable = false;
    }
}

