using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour {
    public enum MeterFill { Gain, Lose }

    [SerializeField] List<MeterFill> meterTypes = new List<MeterFill>();
    [SerializeField] List<float> meterTimers = new List<float>();

    [SerializeField] Slider energyMeter;
    [SerializeField] float uiEnergyMeterFillTime;
    [SerializeField] AnimationCurve fillColor;
    [SerializeField] Image sliderFill;

    [SerializeField] Color sliderFillColor;
    [SerializeField] Color switchWrongColor;
    [SerializeField] Color switchRightColor;


    void Start() {

    }

    void Update() {
        for (int i = 0; i < meterTimers.Count;) {
            meterTimers[i] -= Time.deltaTime;
            if (meterTimers[i] < 0) {
                meterTimers.RemoveAt(i);
                meterTypes.RemoveAt(i);
            } else {
                if (meterTypes[i] == MeterFill.Gain)
                    GainScore(30f);
                else if (meterTypes[i] == MeterFill.Lose)
                    LoseScore(30f);
                i++;
            }
        }
    }

    public void SwitchRight(MeterFill type) {
        if (type == MeterFill.Lose) {
            Debug.LogError("Wrong type used in switch animation");
            return;
        }
        meterTimers.Add(uiEnergyMeterFillTime);
        meterTypes.Add(type);
        //StartCoroutine(ChangeMeterColor(sliderFillColor, switchRightColor));
    }

    public void SwitchWrong(MeterFill type) {
        if (type == MeterFill.Gain) {
            Debug.LogError("Wrong type used in switch animation");
            return;
        }
        meterTimers.Add(uiEnergyMeterFillTime);
        meterTypes.Add(type);
        //StartCoroutine(ChangeMeterColor(sliderFillColor, switchWrongColor));
    }

    //public IEnumerator ChangeMeterColor(Color currentColor, Color newColor) {
    //    //modify scoreslider

    //    float fillTime = uiEnergyMeterFillTime;
    //    float t = 0f;
    //    float fillSpeed = 1 / fillTime;

    //    while (t <= 1) {
    //        t += fillSpeed * Time.deltaTime;
    //        var curvedT = fillColor.Evaluate(t);
    //        sliderFill.color = Color.Lerp(currentColor, newColor, curvedT);
    //        yield return null;
    //    }
    //}

    void GainScore(float amount) {
        energyMeter.value -= amount * Time.deltaTime;
    }

    void LoseScore(float amount) {
        energyMeter.value += amount * Time.deltaTime;
    }
}
