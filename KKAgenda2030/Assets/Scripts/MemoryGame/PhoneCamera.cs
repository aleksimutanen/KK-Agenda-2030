using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    bool camAvailable;
    WebCamTexture frontCam;
    Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    


    void Start() {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0) {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++) {
            if (devices[i].isFrontFacing) {
                frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }
        if (frontCam == null) {
            Debug.Log("Unable to find back camera");
            return;
        }

        frontCam.Play();
        background.texture = frontCam;
        camAvailable = true;
    }

    void Update() {
        if (!camAvailable) {
            return;
        }
        float ratio = (float)frontCam.width / (float)frontCam.height;
        fit.aspectRatio = ratio;

        float scaleY = frontCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -frontCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);


    }
}
