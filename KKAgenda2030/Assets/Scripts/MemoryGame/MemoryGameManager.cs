using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour {

    public List<Texture2D> playerPhotoTextures;
    public List<RawImage> playerImages;
    public int playerCount;
    public int pictureIndx;
    public List<GameObject> playerAvatars;
    List<AvatarClickBehaviour> acb;

    void Start() {
        acb = new List<AvatarClickBehaviour>();
        foreach (var item in playerAvatars) {
            acb.Add(item.GetComponent<AvatarClickBehaviour>());
        }

    }

    void Update() {

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
}
