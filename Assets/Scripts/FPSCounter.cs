using System;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

}
