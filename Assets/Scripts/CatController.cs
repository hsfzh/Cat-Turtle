using UnityEngine;

public class CatPlayer : Player
{
    public bool climb;
    public float delay;
    public bool climbR;

    public override void Initialize(SceneDirector s, GameObject d, GameObject p, GameObject[] l, GameObject b, float sp, float j, bool a)
    {
        base.Initialize(s, d, p, l, b, sp, j, a);
        climb = false;
        delay = 0.5f;
        climbR = true;
    }

    public override void Move()
    {
        if (climb && !sDirector.lightMove)
        {
            DetectInput();
            rigid.velocity = Vector2.zero;
            delay -= Time.deltaTime;
            rigid.gravityScale = 10f;
            if (up)
            {
                player.transform.Translate(new Vector2(0, 1 * speed * Time.deltaTime), Space.World);
                delay = 0.5f;
            }
            if (down)
            {
                player.transform.Translate(new Vector2(0,-1*speed*Time.deltaTime),Space.World);
                delay = 0.5f;
            }
            if (climbR)
            {
                if (left)
                {
                    direction = -1;
                }
                else
                {
                    direction = 0;
                }
            }
            else
            {
                if (right)
                {
                    direction=1;
                }
                else
                {
                    direction = 0;
                }
            }
            player.transform.Translate(new Vector2(direction,0)*speed*Time.deltaTime, Space.World);
            if (delay <= 0)
                climb = false;
        }
        else
        {
            base.Move();
        }
    }
}
public class CatController : MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;

    public CatPlayer player;
    public GameObject[] lights;
    public float distance;
    private bool turn, turnBack;

    // Start is called before the first frame update
    private void Start()
    {
        player = self.AddComponent<CatPlayer>();
        player.Initialize(sDirector, director, self, lights, button, speed, jumpForce, false);
        turn = false;
        turnBack = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Debug.DrawRay(transform.position, Vector2.right * distance, Color.red);
            player.SetActive();
            player.LightSpinning();
            if (player.active)
            {
                gameObject.layer = 8;
                player.Move();
                var rayHitR = Physics2D.Raycast(transform.position, Vector2.right, distance, LayerMask.GetMask("Ground"));
                var rayHitL = Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask.GetMask("Ground"));
                if (player.isJump)
                {
                    if (rayHitL.collider != null)
                    {
                        Debug.Log("감지");
                        if (player.left)
                        {
                            player.climbR = false;
                            player.climb = true;
                            player.delay = 0.5f;
                        }
                    }
                    else if (rayHitR.collider != null)
                    {
                        if (player.right)
                        {
                            player.climbR = true;
                            player.climb = true;
                            player.delay=0.5f;
                        }
                    }
                    else
                    {
                        player.climb = false;
                    }
                }
                if(player.climb)
                {
                    turnBack = false;
                    if (rayHitL.collider != null)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, -90);
                        if (!turn)
                        {
                            turn = true;
                        }
                    }
                    if (rayHitR.collider != null)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 90);
                        if (!turn)
                        {
                            turn = true;
                        }
                    }
                }
                else
                {
                    turn = false;
                    player.rigid.gravityScale = 4.5f;
                    if (!turnBack)
                    {
                        if (player.up)
                        {
                            player.transform.Translate(Vector2.up*10f, Space.World);
                        }
                        turnBack = true;
                    }
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    player.delay = 0.5f;
                }
            }
            else
            {
                gameObject.layer = 7;
            }
        }
    }
}