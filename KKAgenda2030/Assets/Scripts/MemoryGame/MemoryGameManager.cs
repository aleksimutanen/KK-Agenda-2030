using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour {
    public AnimationCurve pairSlideCurve;
    public int playerCount;
    public int pictureIndx;
    public List<RawImage> playerRawImages;
    public List<GameObject> playerAvatars;
    List<AvatarClickBehaviour> acb;
    public Button continueArrow;

    public Sprite[] cardFace;
    public Sprite[] cardFace2;
    public Sprite cardBack;
    public GameObject[] cards;

    bool _init = false;
    public int _matches = 6;

    public GameObject SelfiePanel;
    public RectTransform ReferencePos, ReferencePos2;
    RectTransform MatchCardPos, MatchCardPos2;
    int lastFoundPairValue = -1; // muuttuva cardValue arvo minkä mukaan laitetaan selfiekuvat talteen oikeaan pariin nähden
    public Texture[] selfieTextures = new Texture[6];
    

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

    // CAMERA CAPTURE FUNCTIONALITY
    public void SetPlayerTexture(Texture2D tex) {
        Texture2D texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.SetPixels(tex.GetPixels());
        texture.Apply();
        // Kuvanottoflashi tähän?
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

    //CAMERA FOR SELFIES
    public void SetSelfieTexture(Texture2D tex) {
        Texture2D texture = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
        texture.SetPixels(tex.GetPixels());
        texture.Apply();
        // Kuvanottoflashi tähän?
        selfieTextures[lastFoundPairValue] = texture;

    }

    // CARDS FUNCTIONALITY
    void initializeCards() {
        for (int j = 0; j < 6; j++) {
            for (int i = 0; i < 2; i++) {
                bool test = false;
                int choice = 0;
                while (!test) {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<CardBehaviour>().initialized);
                }
                var card = cards[choice].GetComponent<CardBehaviour>();
                card.cardValue = j;
                card.initialized = true;
                card.setupGraphics(i == 0 ? cardFace[j] : cardFace2[j]);
            }
        }

        if (!_init) {
        _init = true;
        }
    }

    public Sprite getCardBack() {
        return cardBack;
    }

    public void checkCards() {
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
        int x = 0;
        if (cards[c[0]].GetComponent<CardBehaviour>().cardValue == cards[c[1]].GetComponent<CardBehaviour>().cardValue) {
            x = 2;
            MatchCardPos = cards[c[0]].GetComponent<RectTransform>();
            MatchCardPos2 = cards[c[1]].GetComponent<RectTransform>();
            lastFoundPairValue = cards[c[0]].GetComponent<CardBehaviour>().cardValue;
            _matches--;
            print("pari löytyi!");
            // kameran startti ja kuvan ottaminen here?
            TakingSelfie();
            if (_matches == 0) {
                print("Kaikki parit löydetty");
            }
        }
        for (int i = 0; i < c.Count; i++) {
            cards[c[i]].GetComponent<CardBehaviour>().state = x;
            cards[c[i]].GetComponent<CardBehaviour>().falseCheck();
        }

    }

    // SELFIE FUNCTIONALITY    
    void TakingSelfie() {
        SelfiePanel.SetActive(true);
        StartCoroutine(TakePictureCoroutine());
    }

    IEnumerator TakePictureCoroutine() {
        // siirrä valitut parit selfiepanelin lapseksi, disabloi niistä button
        MatchCardPos.gameObject.transform.parent = SelfiePanel.transform;
        MatchCardPos2.gameObject.transform.parent = SelfiePanel.transform;
        MatchCardPos.gameObject.GetComponent<Button>().enabled = false;
        MatchCardPos2.gameObject.GetComponent<Button>().enabled = false;

        // käynnistä kamera erikseen?


        // muita juttuja?
        var t = 0f;
        while (t < 1) {
            t += Time.deltaTime * .2f;
            var newT = pairSlideCurve.Evaluate(t);
            MatchCardPos.position = Vector3.Lerp(MatchCardPos.position, ReferencePos.position, newT);
            MatchCardPos2.position = Vector3.Lerp(MatchCardPos2.position, ReferencePos2.position, newT);
            yield return null;
        }
    }

    // when player pictures are choosed, start coroutine from button
    IEnumerator StartGame() {

        return null;
    }
}
