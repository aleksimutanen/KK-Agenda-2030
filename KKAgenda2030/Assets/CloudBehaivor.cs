using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaivor : MonoBehaviour {

    WeatherToggle wt;

    private void Start() {
        wt = GameObject.Find("MenuContainer").GetComponent<WeatherToggle>();
    }


    public void FadeClouds() {
        GetComponent<AnimatorTimer>().enabled = false;
        GetComponent<Animator>().Play("CloudFade");
        wt.RemoveCloud();
    }

}
