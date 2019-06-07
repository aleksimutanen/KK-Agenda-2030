using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour {

    public MemoryGameManager mgm;
    public RawImage rawimage;  //Image for rendering what the camera sees.
    WebCamTexture webcamTexture = null;
    public GameObject webcamPrefab;
    WebCam _cam;
    WebCam cam {
        get {
            if (_cam == null) {
                _cam = FindObjectOfType<WebCam>();
                if (_cam == null) {
                    var g = Instantiate(webcamPrefab);
                    DontDestroyOnLoad(g);
                    _cam = g.GetComponent<WebCam>();
                }
            }
            return _cam;
        }
    }

    public UIManager_MemoryGame uiM;

    void Start() {
        //mgm = FindObjectOfType<MemoryGameManager>();
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
        uiM.CameraButton.interactable = false;
        mgm.CameraFlash();
        mgm.SetPlayerTexture(GetCamPicture());
        cam.tex.Stop();
        StartCoroutine(CameraRestart());
    }

    public void SetSelfieTextureFromCam() {
        mgm.CameraFlash();
        mgm.SetSelfieTexture(GetCamPicture());
        cam.tex.Stop();
        StartCoroutine(SelfieCameraRestart());
        mgm.SwitchTurn();
    }

    public void SetPlaceholderTexture() {
        mgm.SetSelfieTexture(mgm.placeHolderTexture);
        mgm.SwitchTurn();
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
        uiM.CameraButton.interactable = true;
    }

    IEnumerator SelfieCameraRestart() {
        yield return new WaitForSeconds(1.5f);
        cam.tex.Play();
    }

    // voi ottaa monta kuvaa, joista viimesin tallentuu aina aktiivisena olevalle pelaajalle
    // klikkaamalla avataria vaihdetaan muokattavaa kuvaa
    // kun molemmissa avatareissa valittu joku kuva niin "jatka" nappi enabloituu, silti voi ottaa vielä uusia kuvia
}

