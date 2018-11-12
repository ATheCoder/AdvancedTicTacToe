using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public float[] PosX = { -2.5f, 0f, 2.5f, -2.5f, 0f, 2.5f, -2.5f, 0f, 2.5f};
    public float[] PosY = { 2.5f, 2.5f, 2.5f, 0f, 0f, 0f, -2.5f, -2.5f, -2.5f };
    public int boardNumber = 0;
    public int currentCameraSize = 4;
    private bool isZoomout = false;
    private Camera kamera;
    // Use this for initialization
    void Start () {
        kamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isZoomout) {
            kamera.orthographicSize = Mathf.Lerp(kamera.orthographicSize, currentCameraSize, 10f * Time.deltaTime);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PosX[boardNumber], PosY[boardNumber], transform.position.z), 0.5f);
        kamera.orthographicSize = Mathf.Lerp(kamera.orthographicSize, currentCameraSize, 10f * Time.deltaTime);
    }
    public void setCameraPos(int boardNumber1) {
        boardNumber = boardNumber1;
    }
    public void pickMode(bool mode) {
        if (mode) {
           currentCameraSize = 1;
        }
        else {
            currentCameraSize = 4;
        }
    }
    public void ZoomOut() {
        isZoomout = true;
        pickMode(false);
        transform.position = new Vector3(0, 0, -10);
    }
    public void ZoomIn() {
        isZoomout = false;
        pickMode(true);
    }
    public void GameEnd() {
        kamera.orthographicSize = 5;
        transform.position = new Vector3(0, 0, -10);
    }
}
