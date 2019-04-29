using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour {


    public WebCamTexture tex = null;

    public void Awake() {
        WebCamDevice[] camDevices = WebCamTexture.devices;
        string camName = camDevices[0].name;
        tex = new WebCamTexture(camName);
    }


}
