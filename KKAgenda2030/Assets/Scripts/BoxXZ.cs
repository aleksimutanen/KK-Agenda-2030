using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxXZ : MonoBehaviour {

    public Vector3 size;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
