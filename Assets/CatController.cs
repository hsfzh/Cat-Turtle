using UnityEngine;

public class CatPlayer : Player
{
    public bool climb;

    public CatPlayer(GameObject d, GameObject p, GameObject b, float s, float j, bool a) : base(d, p, b, s, j, a)
    {
        climb = false;
    }
    public override void Move()
    {
        base.Move();
        //고양이 기어올라가기
        if (climb)
        {
            rigid.velocity=Vector2.zero;
            rigid.gravityScale = 10f;
            if (up)
            {
                player.transform.Translate(new Vector2(0, 1 * speed * Time.deltaTime), Space.World);
            }

            if (down)
            {
                player.transform.Translate(new Vector2(0,-1*speed*Time.deltaTime),Space.World);
            }
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

    private CatPlayer player;
    private float distance;
    private bool turn;

    // Start is called before the first frame update
    private void Start()
    {
        player = new CatPlayer(director, self, button, speed, jumpForce, false);
        turn = false;
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
                        if(player.left)
                            player.climb = true;
                    }
                    else if (rayHitR.collider != null)
                    {
                        if(player.right)
                            player.climb = true;
                    }
                    else
                    {
                        player.climb = false;
                    }
                }
                if(player.climb)
                {
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
                    distance = 3.5f;
                }
                else
                {
                    turn = false;
                    distance = 7.5f;
                    player.rigid.gravityScale = 4.5f;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else
            {
                gameObject.layer = 7;
            }
        }
    }
}