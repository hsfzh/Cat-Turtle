using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float screenRatio;

    private float refRatio = 1920f / 1080f;

    private float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        screenRatio = (float)Screen.width / (float)Screen.height;
        if (screenRatio < refRatio)
        {
            Camera.main.orthographicSize = 1920f / screenRatio / 20f;
        }
        else
        {
            Camera.main.orthographicSize = 1080f / 20f;
        }
        cameraSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.orthographicSize > cameraSize)
            Camera.main.orthographicSize = cameraSize;
    }
}
