using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggler : MonoBehaviour
{
    public List<Image> images;
    public int selectedImage;

    EnergyMeter em;
    ToggleCollector tc;

    void Start() {
        em = FindObjectOfType<EnergyMeter>();
        tc = FindObjectOfType<ToggleCollector>();
    }

    public void changeImage() {
        images[selectedImage].gameObject.SetActive(false);
        selectedImage = 1 - selectedImage;
        images[selectedImage].gameObject.SetActive(true);
        if (selectedImage == 0) {
            em.SwitchWrong(EnergyMeter.MeterFill.Lose);
            tc.SwitchWrong();
        } else {
            em.SwitchRight(EnergyMeter.MeterFill.Gain);
            tc.SwitchRight();
        }
    }
}
