using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool left, right, up, down;

    // Start is called before the first frame update
    private void Start()
    {
        left = false;
        right = false;
        up = false;
        down = false;
    }

    // Update is called once per frame
    private void Update()
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

    public void DownDown()
    {
        down = true;
    }

    public void DownUp()
    {
        down = false;
    }
}