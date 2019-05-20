using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour {

    WebCamTexture _tex;
    public WebCamTexture tex {
        get {
            if (_tex == null) {
                WebCamDevice[] camDevices = WebCamTexture.devices;
                string camName = camDevices[0].name;
                _tex = new WebCamTexture(camName);
            }
            return _tex;
        }
    }
}
