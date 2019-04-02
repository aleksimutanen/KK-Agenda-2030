using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour { 


    public void TakeAShot() {
        StartCoroutine("CaptureThis");
    }

    IEnumerator CaptureThis() {
        string timeStamp = System.DateTime.Now.ToString("dd-mm-yyyy-HH-mm-ss");
        string fileName = "screenshot_" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
    }


}
