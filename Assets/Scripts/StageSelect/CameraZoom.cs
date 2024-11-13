using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float cameraSpeed;

    public float orthoZoomSpeed = 0.5f;
    
    private float screenRatio;

    private float refRatio = 2997f / 1665f;

    private float cameraSize;
    private float curCameraSize;

    private Vector2 curPos;
    private Vector2 prevPos;
    private Vector2 direc;
    public bool x, y;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        x = y = true;
        prevPos = new Vector2(-99999, -99999);
        screenRatio = (float)Screen.width / (float)Screen.height;
        if (screenRatio < refRatio)
        {
            Camera.main.orthographicSize = 2997f / screenRatio / 20f/2;
        }
        else
        {
            Camera.main.orthographicSize = 1665f / 20f/2;
        }
        cameraSize = Camera.main.orthographicSize;
    }
    void Update()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);
            
            var touchZeroPrevPos= touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            
            var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
        }
        curCameraSize = Camera.main.orthographicSize;
        x = (transform.position.x - curCameraSize * screenRatio > -299.7f / 2f) &&
            (transform.position.x + curCameraSize * screenRatio < 299.7f / 2f);
        y = (transform.position.y - curCameraSize > -166.5f / 2f) &&
            (transform.position.y + curCameraSize <166.5f / 2f);
        if (x && y)
        {
            if (prevPos == new Vector2(-99999, -99999) && Input.GetMouseButton(0))
            {
                prevPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                curPos = Input.mousePosition;
                if (curPos != prevPos)
                {
                    direc = curPos - prevPos;
                    transform.Translate(direc.normalized * cameraSpeed * Time.deltaTime);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            prevPos = new Vector2(-99999, -99999);
        }
        pos = transform.position;
        BoundCamera();
        if (Camera.main.orthographicSize > cameraSize)
            Camera.main.orthographicSize = cameraSize;
    }

    public void BoundCamera()
    {
        pos.x = Mathf.Clamp(pos.x, -299.7f / 2f + curCameraSize * screenRatio + 0.1f, 299.7f / 2f - curCameraSize * screenRatio - 0.1f);
        pos.y = Mathf.Clamp(pos.y, -166.5f / 2f + curCameraSize + 0.1f, 166.5f / 2f - curCameraSize - 0.1f);
        transform.position = pos;
    }
}