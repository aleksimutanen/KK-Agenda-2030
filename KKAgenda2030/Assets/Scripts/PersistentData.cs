using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public float totalStarAmount;
    public int pageIndex = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


}
