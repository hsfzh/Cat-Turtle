using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    public GameObject menuSet;
    // Start is called before the first frame update
    private void Start()
    {
        menuSet.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (menuSet.activeSelf)
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
}