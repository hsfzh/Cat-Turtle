using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject ResetSet;
    public GameObject ClearSet;
    public bool lightMove;
    public float tileSize;
    public Transform player;
    public Rigidbody2D slime, cat, turtle;
    // Start is called before the first frame update
    private void Start()
    {
        menuSet.SetActive(false);
        ResetSet.SetActive(false);
        ClearSet.SetActive(false);
        lightMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (menuSet.activeSelf || ResetSet.activeSelf || ClearSet.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (ClearSet.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Stage select");
            }
        }

        if (Time.timeScale != 0)
        {
            CheckPlayer();
            CheckClear();
        }
    }

    public void CheckClear()
    {
        if (player.position.x >= (60 - tileSize/2f))
        {
            ClearSet.SetActive(true);
        }
    }

    public void CheckPlayer()
    {
        if (slime.velocity != Vector2.zero)
        {
            player = slime.transform;
        }else if (cat.velocity != Vector2.zero)
        {
            player = cat.transform;
        }else if (turtle.velocity != Vector2.zero)
        {
            player = turtle.transform;
        }
    }
    
    public void OpenMenu()
    {
        menuSet.SetActive(true);
    }

    public void CloseMenu()
    {
        menuSet.SetActive(false);
    }
    public void OpenReset()
    {
        ResetSet.SetActive(true);
    }
    public void CloseReset()
    {
        ResetSet.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start");
    }
}