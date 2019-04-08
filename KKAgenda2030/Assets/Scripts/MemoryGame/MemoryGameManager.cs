using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour {

    public int playerCount;
    public int pictureIndx;
    public List<RawImage> playerRawImages;
    public List<GameObject> playerAvatars;
    List<AvatarClickBehaviour> acb;

    public List<GameObject> cardsList;

    public Button continueArrow;

    void Start() {
        acb = new List<AvatarClickBehaviour>();
        foreach (var item in playerAvatars) {
            acb.Add(item.GetComponent<AvatarClickBehaviour>());
        }
        SlotSelected(0);
    }

    void Update() {

    }

    public void SetPlayerTexture(Texture2D tex) {
        Texture2D texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.SetPixels(tex.GetPixels());
        texture.Apply();
        playerRawImages[pictureIndx].GetComponent<RawImage>().texture = texture;
        bool foundEmpty = false;
        for (int i = 0; i < playerRawImages.Count; i++) {
            var ri = playerRawImages[i].GetComponent<RawImage>();
            if (ri.texture == null) {
                foundEmpty = true;
                SlotSelected(i);
                break;
            }
        }
        if (!foundEmpty) {
            continueArrow.interactable = true;
        }
    }


    public void SlotSelected(int i) {
        if (pictureIndx == i) {
            return;
        }
        pictureIndx = i;
        for (int j = 0; j < acb.Count; j++) {
            if (pictureIndx == j) {
                acb[j].OnSelected();
            } else {
                acb[j].OnDeselected();
            }
        }
    }

    // when player pictures are choosed, start coroutine from button
    IEnumerator StartGame() {

        return null;
    }
}
