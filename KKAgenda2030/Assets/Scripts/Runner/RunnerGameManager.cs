using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerGameManager : MonoBehaviour {

    public static RunnerGameManager instance;

    public enum TimerType { GainSmall, GainBig, LoseSmall, LoseBig }


    // Game logic
    [SerializeField] List<TimerType> timerTypes = new List<TimerType>();
    [SerializeField] List<float> scoreTimers = new List<float>();

    public float timerLength;

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
    void Start() {
        if (instance)
            Debug.LogError("2+ RunnerManagers found!");
        instance = this;
        maxLives = livesLeft;
    }

    void Update() {

        for (int i = 0; i < scoreTimers.Count;) {
            scoreTimers[i] -= Time.deltaTime;
            if (scoreTimers[i] < 0) {
                scoreTimers.RemoveAt(i);
                timerTypes.RemoveAt(i);
            } else {
                if (timerTypes[i] == TimerType.LoseSmall)
                    LoseScore(loseSmallScore / timerLength);
                else if (timerTypes[i] == TimerType.LoseBig)
                    LoseScore(loseBigScore / timerLength);
                else if (timerTypes[i] == TimerType.GainSmall)
                    GainScore(gainSmallScore / timerLength);
                else if (timerTypes[i] == TimerType.GainBig)
                    GainScore(gainBigScore / timerLength);
                i++;
            }
        }
    }

    public void HitCollectable(TimerType type) {
        if (type == TimerType.LoseBig || type == TimerType.LoseSmall) {
            Debug.LogError("Wrong type used in collectable");
            return;
        }
        scoreTimers.Add(timerLength);
        timerTypes.Add(type);
        StartCoroutine(HitGameObject(sliderFillColor, hitCollectableColor));
    }

    public void HitAvoidable(TimerType type) {
        if (type == TimerType.GainBig || type == TimerType.GainSmall) {
            Debug.LogError("Wrong type used in avoidable");
            return;
        }
        scoreTimers.Add(timerLength);
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
        float fillTime = timerLength;
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

    public void LoseLife() {
        livesLeft--;
        livesLeftText.text = livesLeft + " / " + maxLives;
        if (livesLeft == 0) {

        }
    }
}

