using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherToggle : MonoBehaviour {

    public int totalClouds;
    public GameObject rainy;
    public GameObject sunny;
    public GameObject rainParticles;
    public AudioSource audioSource;
    public AudioClip clip;



    public void RemoveCloud() {
        totalClouds--;
        if (totalClouds == 0) {
            ChangeWeather();
        }
    }

    void ChangeWeather() {
        rainy.SetActive(false);
        sunny.SetActive(true);
        rainParticles.SetActive(false);
        audioSource.clip = clip;
        audioSource.volume = 0.4f;
        audioSource.Play();
        GetComponent<AudioSource>().Play();
    }

}
