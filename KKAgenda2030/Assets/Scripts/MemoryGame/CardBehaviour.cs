using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour {

    [SerializeField] int _state;
    [SerializeField] int _cardValue;
    [SerializeField] bool _initialized = false;

    Sprite cardBack;
    Sprite cardFace;

    public MemoryGameManager mgm;

    void Awake() {
        _state = 1;
    }

    public void setupGraphics(Sprite s) {
        cardBack = mgm.getCardBack();
        cardFace = s;
        flipCard();

    }

    public void flipCard() {
        if (_state == 0) {
            _state = 1;
        }
        else if(_state == 1) {
            _state = 0;
        }
        if (_state == 0) {
            GetComponent<Image>().sprite = cardBack;
        }
        else if (_state == 1) {
            GetComponent<Image>().sprite = cardFace;
        }
    }

    public int cardValue {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public int state {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized {
        get { return _initialized; }
        set { _initialized = value; }
    }

    public void falseCheck() {
        StartCoroutine(Delay());
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(1);
        if (_state == 0) {
            GetComponent<Image>().sprite = cardBack;
        }
        else if (_state == 1) {
            GetComponent<Image>().sprite = cardFace;
        }
    }
}
