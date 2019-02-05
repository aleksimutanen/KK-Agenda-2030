using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public float speed;

    void Start() {

    }

    void Update() {
        speed += Time.deltaTime * 0.1f;
        transform.position += -transform.right * speed * Time.deltaTime;
    }
}
