using UnityEngine;

public class CatPlayer : Player
{
    public bool climb;
    public float delay;
    public bool climbR;

    public override void Initialize(GameObject d, GameObject p, GameObject b, float s, float j, bool a)
    {
        base.Initialize(d, p, b, s, j, a);
        climb = false;
        delay = 0.5f;
        climbR = true;
    }

    public override void Move()
    {
        if (climb)
        {
            DetectInput();
            rigid.velocity = Vector2.zero;
            Debug.Log(delay);
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
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;

    public CatPlayer player;
    private float distance;
    private bool turn, turnBack;

    // Start is called before the first frame update
    private void Start()
    {
        player = self.AddComponent<CatPlayer>();
        player.Initialize(director, self, button, speed, jumpForce, false);
        turn = false;
        turnBack = false;
        distance = 7.5f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Debug.DrawRay(transform.position, Vector2.right * distance, Color.red);
            player.SetActive();
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
                            transform.position += new Vector3(-4f, 0, 0);
                            turn = true;
                        }
                    }
                    if (rayHitR.collider != null)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 90);
                        if (!turn)
                        {
                            transform.position += new Vector3(4f, 0, 0);
                            turn = true;
                        }
                    }
                    distance = 3.75f;
                }
                else
                {
                    turn = false;
                    distance = 7.5f;
                    player.rigid.gravityScale = 4.5f;
                    if (!turnBack)
                    {
                        if (player.up)
                        {
                            player.transform.Translate(Vector2.up*5f, Space.World);
                        }
                        if (transform.localRotation == Quaternion.Euler(0, 0, -90))
                        {
                            transform.position += new Vector3(4f, 0, 0);
                        }
                        else if (transform.localRotation == Quaternion.Euler(0, 0, 90))
                        {
                            transform.position += new Vector3(-4f, 0, 0);
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