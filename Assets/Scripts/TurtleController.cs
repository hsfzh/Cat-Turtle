using UnityEngine;

public class TurtlePlayer : Player
{
    public bool tunnel;
    public override void Initialize(SceneDirector s, GameObject d, GameObject p, GameObject[] l, GameObject b, float sp,
        float j, bool a, float h)
    {
        base.Initialize(s, d, p, l, b, sp, j, a, h);
        tunnel = false;
    }
}
public class TurtleController : MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;
    public float height;
    public bool up, down;

    public TurtlePlayer player;
    public GameObject[] lights;

    // Start is called before the first frame update
    private void Start()
    {
        player = self.AddComponent<TurtlePlayer>();
        player.Initialize(sDirector, director, self, lights, button, speed, jumpForce, false, height);
        up = false;
        down = true;
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
                var rayHitD = Physics2D.Raycast(transform.position, Vector2.down, player.height*1.5f, LayerMask.GetMask("Ground", "Tile"));
                var rayHitU = Physics2D.Raycast(transform.position, Vector2.up, player.height*1.5f, LayerMask.GetMask("Ground", "Tile"));
                if (rayHitD.collider != null)
                    down = true;
                else
                    down = false;
                if (rayHitU.collider != null)
                    up = true;
                else
                    up = false;
                if (up&&down)
                {
                    player.tunnel = true;
                }
                else
                {
                    player.tunnel = false;
                }
                player.Move();
            }
            else
            {
                gameObject.layer = 7;
                player.tunnel = false;
                player.rigid.velocity=new Vector2(0, player.rigid.velocity.y);
            }
        }
       
    }
}