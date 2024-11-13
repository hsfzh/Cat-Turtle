using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float cameraSpeed = 5f;

    public float orthoZoomSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            var touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            var touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장
            
            var touchZeroPrevPos= touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            
            var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
        }
        else if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            var touchPrevPos = touch.position - touch.deltaPosition;
            Camera.main.transform.Translate(touchPrevPos / touchPrevPos.magnitude * cameraSpeed * Time.deltaTime);
        }
    }
    
}