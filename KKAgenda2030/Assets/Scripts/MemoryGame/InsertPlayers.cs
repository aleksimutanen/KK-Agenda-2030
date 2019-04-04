﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertPlayers : MonoBehaviour {

    MemoryGameManager mgm;
    public GameObject avatarPanel;
    public GameObject player1Avatar;
    public GameObject player2Avatar;


    void Start() {
        mgm = FindObjectOfType<MemoryGameManager>();
    }

    public void Insert1Player() {
        mgm.playerCount = 1;
        gameObject.SetActive(false);
        avatarPanel.SetActive(true);
        player2Avatar.SetActive(false);
    }

    public void Insert2Player() {
        mgm.playerCount = 2;
        gameObject.SetActive(false);
        avatarPanel.SetActive(true);
    }

}
