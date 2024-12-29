using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string stage;
    public int stageNum;
    public PlayerMove player;
    public Transform playerPos;
    public Transform[] points;

    private void Start()
    {
        transform.position = points[stageNum].position;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
            if (Vector3.Distance(touchedPos, transform.position) < 0.1f)
            {
                OnMouseDown();
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(Vector3.Distance(transform.position, playerPos.position));
        if (Vector3.Distance(transform.position, playerPos.position) < 0.1f)
        {
            SceneManager.LoadScene(stage);
        }
        else
        {
            player.Move(stageNum);
        }
    }
}
