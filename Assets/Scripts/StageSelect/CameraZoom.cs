using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float cameraSpeed;

    public float orthoZoomSpeed = 0.5f;
    
    private float screenRatio;

    private float refRatio = 2997f / 1665f;

    private float cameraSize;
    private float curCameraSize;

    private Rigidbody2D rigid;
    private Vector2 curPos;
    private Vector2 prevPos;
    private Vector2 direc;
    private Vector3 pos;
    public int cameraCase;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        prevPos = new Vector2(-99999, -99999);
        screenRatio = (float)Screen.width / (float)Screen.height;
        if (screenRatio < refRatio)
        {
            cameraCase = 0;
            Camera.main.orthographicSize = 2997f / screenRatio / 20f;
        }
        else
        {
            cameraCase = 1;
            Camera.main.orthographicSize = 1665f / 20f;
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
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraSize/3f, cameraSize);
        curCameraSize = Camera.main.orthographicSize;

        if (Input.touchCount < 2 && Camera.main.orthographicSize < cameraSize)
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
                    direc = prevPos - curPos;
                    if (direc.magnitude < cameraSpeed)
                        rigid.velocity = direc;
                    else
                        rigid.velocity = direc.normalized * cameraSpeed;
                }
                else
                {
                    rigid.velocity=Vector2.zero;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                prevPos = new Vector2(-99999, -99999);
                direc=Vector2.zero;
            }
        }
        if (rigid.velocity.magnitude > 0)
        {
            rigid.velocity *= 0.99f;
        }
        else
        {
            rigid.velocity=Vector2.zero;
        }
        pos = transform.position;
        if (Camera.main.orthographicSize < cameraSize)
        {
            if(Camera.main.orthographicSize>1665f/20f && cameraCase==0)
                BoundCameraX();
            else if(Camera.main.orthographicSize>2997f/(screenRatio)/20f && cameraCase==1)
                BoundCameraY();
            else
                BoundCamera();
        }
        else
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }

    public void BoundCamera()
    {
        pos.x = Mathf.Clamp(pos.x, -299.7f / 2f + curCameraSize * screenRatio + 0.1f, 299.7f / 2f - curCameraSize * screenRatio - 0.1f);
        pos.y = Mathf.Clamp(pos.y, -166.5f / 2f + curCameraSize + 0.1f, 166.5f / 2f - curCameraSize - 0.1f);
        transform.position = pos;
    }

    public void BoundCameraX()
    {
        pos.x = Mathf.Clamp(pos.x, -299.7f / 2f + curCameraSize * screenRatio + 0.1f, 299.7f / 2f - curCameraSize * screenRatio - 0.1f);
        pos.y = 0;
        transform.position = pos;
    }
    
    public void BoundCameraY()
    {
        pos.x = 0;
        pos.y = Mathf.Clamp(pos.y, -166.5f / 2f + curCameraSize + 0.1f, 166.5f / 2f - curCameraSize - 0.1f);
        transform.position = pos;
    }
}