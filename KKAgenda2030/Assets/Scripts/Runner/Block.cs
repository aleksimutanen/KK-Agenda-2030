﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
            print("xd");
    }
}
