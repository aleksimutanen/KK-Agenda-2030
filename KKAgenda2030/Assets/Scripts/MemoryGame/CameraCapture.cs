using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour {

    MemoryGameManager mgm;
    public RawImage rawimage;  //Image for rendering what the camera sees.
    WebCamTexture webcamTexture = null;
    public WebCam cam;

    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
        rawimage.texture = cam.tex;
        cam.tex.Play();
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
        cam.tex.Stop();
        StartCoroutine(CameraRestart());
    }

    public void SetSelfieTextureFromCam() {
        mgm.CameraFlash();
        mgm.SetSelfieTexture(GetCamPicture());
        cam.tex.Stop();
        StartCoroutine(CameraRestart());

    }

    public void SetPlaceholderTexture() {
        mgm.SkipSelfie(mgm.placeHolderTexture);
    }

    Texture2D GetCamPicture() {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(rawimage.texture.width, rawimage.texture.height, TextureFormat.ARGB32, false);
        texture.SetPixels(cam.tex.GetPixels()); // Save the image to the Texture2D
        texture.Apply();
        return texture;
    }

    IEnumerator CameraRestart() {
        yield return new WaitForSeconds(1.5f);
        cam.tex.Play();
    }

    // voi ottaa monta kuvaa, joista viimesin tallentuu aina aktiivisena olevalle pelaajalle
    // klikkaamalla avataria vaihdetaan muokattavaa kuvaa
    // kun molemmissa avatareissa valittu joku kuva niin "jatka" nappi enabloituu, silti voi ottaa vielä uusia kuvia
}

