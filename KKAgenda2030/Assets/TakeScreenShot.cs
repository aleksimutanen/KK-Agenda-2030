using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeScreenShot : MonoBehaviour
{
   
    public GameObject photo;
    public List<GameObject> uiElements = new List<GameObject>();


    public void TakeAShot()
    {
        StartCoroutine("CaptureIt");
    }

    IEnumerator CaptureIt()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;

        foreach (var ele in uiElements)
        {
            ele.SetActive(false);
        }


        ScreenCapture.CaptureScreenshot(pathToSave);

        photo.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        photo.gameObject.SetActive(false);


        foreach (var ele in uiElements)
        {

            ele.SetActive(true);
        }

        print("Kuva otettu");

    }

}
