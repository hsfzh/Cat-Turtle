using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool left, right, up;
    // Start is called before the first frame update
    void Start()
    {
        left = false;
        right = false;
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LeftDown()
    {
        left = true;
    }
    public void LeftUp()
    {
        left = false;
    }
    public void RightDown()
    {
        right = true;
    }
    public void RightUp()
    {
        right = false;
    }
    public void UpDown()
    {
        up = true;
    }
    public void UpUp()
    {
        up = false;
    }
}
