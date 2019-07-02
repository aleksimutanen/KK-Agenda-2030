using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page6Reset : MonoBehaviour {

    public AnimationManager_MemoryGame amm;

    private void OnEnable() {
        amm.ResetMinigame();
    }
}
