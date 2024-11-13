using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject ResetSet;
    public bool lightMove;
    // Start is called before the first frame update
    private void Start()
    {
        menuSet.SetActive(false);
        ResetSet.SetActive(false);
        lightMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (menuSet.activeSelf || ResetSet.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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