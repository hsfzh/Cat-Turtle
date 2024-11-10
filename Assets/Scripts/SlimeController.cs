using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;
    public float height;
    private Player player;
    public GameObject[] lights;

    // Start is called before the first frame update
    private void Start()
    {
        player = self.AddComponent<Player>();
        player.Initialize(sDirector, director, self, lights, button, speed, jumpForce, false, height);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            player.SetActive();
            player.LightSpinning();
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