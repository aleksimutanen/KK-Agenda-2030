using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public float totalStarAmount;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


}
