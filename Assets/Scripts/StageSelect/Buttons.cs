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
