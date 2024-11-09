using System;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        Debug.Log(1/Time.deltaTime);
    }
}
