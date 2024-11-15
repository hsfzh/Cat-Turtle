using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string stageNum;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(stageNum);
    }
}
