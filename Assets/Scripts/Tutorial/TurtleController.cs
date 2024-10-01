using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;

    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = self.AddComponent<Player>();
        player.Initialize(sDirector, director, self, button, speed, jumpForce, false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            player.SetActive();
            if (player.active)
            {
                gameObject.layer = 8;
                player.Move();
            }
            else
            {
                gameObject.layer = 7;
            }
        }
       
    }
}