﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour {

    MemoryGameManager mgm;
    public RawImage rawimage;  //Image for rendering what the camera sees.
    WebCamTexture webcamTexture = null;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();

        //Save get the camera devices, in case you have more than 1 camera.
        WebCamDevice[] camDevices = WebCamTexture.devices;

        //Get the used camera name for the WebCamTexture initialization.
        string camName = camDevices[0].name;
        webcamTexture = new WebCamTexture(camName);

        //Render the image in the screen.
        rawimage.texture = webcamTexture;
        webcamTexture.Play();
    }

    void Update() {
        // Reset Camera for next picture, DEBUG USE
        //if (Input.GetMouseButtonDown(1)) {
        //    rawimage.texture = webcamTexture;
        //    rawimage.material.mainTexture = webcamTexture;
        //    webcamTexture.Play();
        //}
    }

    public void SetPlayerTextureFromCam() {
        mgm.CameraFlash();
        mgm.SetPlayerTexture(GetCamPicture());
    }

    public void SetSelfieTextureFromCam() {
        mgm.CameraFlash();
        mgm.SetSelfieTexture(GetCamPicture());
    }

    Texture2D GetCamPicture() {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);
        texture.SetPixels(webcamTexture.GetPixels()); // Save the image to the Texture2D
        texture.Apply();
        return texture;
    }

    // voi ottaa monta kuvaa, joista viimesin tallentuu aina aktiivisena olevalle pelaajalle
    // klikkaamalla avataria vaihdetaan muokattavaa kuvaa
    // kun molemmissa avatareissa valittu joku kuva niin "jatka" nappi enabloituu, silti voi ottaa vielä uusia kuvia
}

