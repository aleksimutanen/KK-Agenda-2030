using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour {

    public List<Texture2D> playerPhotoTextures;
    public List<RawImage> playerImages;
    public int pictureIndx;

    void Start() {

    }

    void Update() {

    }

    public void SaveAvatarPicture() {
        //playerImages[pictureIndx].GetComponent<RawImage>().texture = playerPhotoTextures[0];
        //playerPhotoTextures.Clear();
        //pictureIndx++;
    }
}
