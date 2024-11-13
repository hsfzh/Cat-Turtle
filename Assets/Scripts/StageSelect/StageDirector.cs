using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    public void Stage1()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
