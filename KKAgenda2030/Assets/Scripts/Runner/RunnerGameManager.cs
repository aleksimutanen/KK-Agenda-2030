using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerGameManager : MonoBehaviour {

    public static RunnerGameManager instance;

    public enum TimerType { GainSmall, GainBig, LoseSmall, LoseBig }


    // Game logic
    [SerializeField] int levelIndex;

    [SerializeField] List<TimerType> timerTypes = new List<TimerType>();
    [SerializeField] List<float> scoreTimers = new List<float>();

    [Range(1f,5f)]public float uiScoreSliderAnimationTime;

    [SerializeField] GameObject[] levels;

    [SerializeField] float gainSmallScore;
    [SerializeField] float gainBigScore;

    [SerializeField] float loseSmallScore;
    [SerializeField] float loseBigScore;

    public int livesLeft;
    int maxLives;

    //

    // UI
    [SerializeField] Slider scoreSlider;
    [SerializeField] Image sliderFill;
    [SerializeField] Text livesLeftText;

    [SerializeField] AnimationCurve fillColor;
    [SerializeField] Color sliderFillColor;
    [SerializeField] Color hitAvoidableColor;
    [SerializeField] Color hitCollectableColor;

    //

    RunnerController character;
    Vector3 mapStartPos;

    void Start() {
        if (instance)
            Debug.LogError("2+ RunnerManagers found!");
        instance = this;
        maxLives = livesLeft;
        character = FindObjectOfType<RunnerController>();
        levelIndex = 0;
    }

    void Update() {

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
            print(curvedT);
            sliderFill.color = Color.Lerp(currentColor, newColor, curvedT);
            yield return null;
            //foreach (SpriteRenderer sr in sharkSprites) sr.color = Color.Lerp(Color.white, ateTrashColor, curvedT);
        }

        sliderFill.color = Color.white;
    }

    public void RestartLevel() {
        //character.transform.position = charStartPos;
        GameObject levelClone = Instantiate(levels[levelIndex], Vector3.zero, transform.rotation);
        Destroy(levels[levelIndex]);
        levels[levelIndex] = levelClone;
    }

    public void LoseLife() {
        livesLeft--;
        livesLeftText.text = livesLeft + " / " + maxLives;
        if (livesLeft == 0) {
            RestartLevel();
        }
    }
}

