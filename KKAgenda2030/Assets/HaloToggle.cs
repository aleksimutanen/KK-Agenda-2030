using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloToggle : MonoBehaviour
{
    public GameObject halo;
    ToyRepairManager trm;
    public Animator lennuAnimator;

    void Start()
    {
        trm = FindObjectOfType<ToyRepairManager>();
    }

    void Update()
    {
        if (trm.toysInPlace == 3) {
            halo.SetActive(true);
            lennuAnimator.Play("Lennu_happy");
            trm.toysInPlace = 0;
        }
    }
}
