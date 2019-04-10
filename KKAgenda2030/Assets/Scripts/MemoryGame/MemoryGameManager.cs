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
    public Button continueArrow;

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;

    bool _init = false;
    public int _matches = 6;



    void Start() {
        acb = new List<AvatarClickBehaviour>();
        foreach (var item in playerAvatars) {
            acb.Add(item.GetComponent<AvatarClickBehaviour>());
        }
        SlotSelected(0);
    }

    void Update() {
        if (!_init) {
            initializeCards();
        }

        if (Input.GetMouseButtonUp(0)) {
            checkCards();
        }
    }

    void initializeCards() {
        for (int i = 0; i < 2; i++) {
            for (int j = 1; j < 7; j++) {

                bool test = false;
                int choice = 0;
                while (!test) {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<CardBehaviour>().initialized);
                }
                cards[choice].GetComponent<CardBehaviour>().cardValue = j;
                cards[choice].GetComponent<CardBehaviour>().initialized = true;
            }
        }
        foreach (var item in cards) {
            item.GetComponent<CardBehaviour>().setupGraphics();
        }
        if (!_init) {
            _init = true;
        }
    }

    public Sprite getCardBack() {
        return cardBack;
    }

    public Sprite getCardFace(int i) {
        return cardFace[i - 1];
    }

    void checkCards() {
        List<int> c = new List<int>();
        for (int i = 0; i < cards.Length; i++) {
            if (cards[i].GetComponent<CardBehaviour>().state == 1) {
                c.Add(i);
            }
        }
        if (c.Count == 2) {
            cardComparison(c);
        }
    }

    void cardComparison(List<int> c) {
        CardBehaviour.DO_NOT = true;
        int x = 0;
        if (cards[c[0]].GetComponent<CardBehaviour>().cardValue == cards[c[1]].GetComponent<CardBehaviour>().cardValue) {
            x = 2;
            _matches--;
            print("pari löytyi!");
            if (_matches == 0) {
                print("Kaikki parit löydetty");
            }
        }
        for (int i = 0; i < c.Count; i++) {
            cards[c[i]].GetComponent<CardBehaviour>().state = x;
            cards[c[i]].GetComponent<CardBehaviour>().falseCheck();
        }
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
