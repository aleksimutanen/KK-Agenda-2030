using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Button pause;

	void Start () {
	    	
	}
	
	void Update () {
		
	}

    public void PauseButton() {
        pause.GetComponent<Animator>().Play("PauseButtonAnimation");
        //GrandManager.instance.Pause();
        print("transition");
    }
}
