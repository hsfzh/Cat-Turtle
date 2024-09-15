using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;
    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = new Player(director, self, button, speed, jumpForce, true);
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