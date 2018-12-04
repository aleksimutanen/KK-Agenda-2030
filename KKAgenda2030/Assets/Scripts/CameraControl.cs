using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    UIManager ui;

    public float x;

    public Transform target;

    public BoxXZ visibleArea;

    //public GameObject pauseMenu;

	void Start () {
        ui = FindObjectOfType<UIManager>();
        //print(Screen.dpi);
	}
	
	void Update () {
        // track
        var newPosition = target.position + Vector3.up * 10f;

        // clamp camera
        var cam = Camera.main;
        var camSizeZ = cam.orthographicSize * 2;
        var camSizeX = cam.pixelWidth / (float)cam.pixelHeight * camSizeZ;

        var camAllowedOffsetX = (visibleArea.size.x - camSizeX) * 0.5f;
        var camAllowedOffsetZ = (visibleArea.size.z - camSizeZ) * 0.5f;

        newPosition.x = Mathf.Clamp(newPosition.x, -camAllowedOffsetX, camAllowedOffsetX);
        newPosition.z = Mathf.Clamp(newPosition.z, -camAllowedOffsetZ, camAllowedOffsetZ);

        transform.position = newPosition;
        //if (!ui.transition)
        //    pauseMenu.transform.position = transform.position + new Vector3((-camSizeX - x) * 0.5f, -5, (camSizeZ + x) * 0.5f);
    }

    private void OnDrawGizmos() {
        var cam = Camera.main;
        var camSizeZ = cam.orthographicSize * 2;
        var camSizeX = cam.pixelWidth / (float)cam.pixelHeight * camSizeZ;

        var camAllowedOffsetX = (visibleArea.size.x - camSizeX) * 0.5f;
        var camAllowedOffsetZ = (visibleArea.size.z - camSizeZ) * 0.5f;

        Gizmos.DrawWireCube(transform.position, new Vector3(camSizeX, 0, camSizeZ));
    }
}
