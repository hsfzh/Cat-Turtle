using UnityEngine;

public class CatPlayer : Player
{
    public bool climb;

    public CatPlayer(GameObject d, GameObject p, GameObject b, float s, float j, bool a, bool c) : base(d, p, b, s, j, a)
    {
        climb = c;
    }
    public override void Move()
    {
        base.Move();
        //고양이 기어올라가기
        if (climb && up)
        {
            Debug.Log("climb");
            rigid.gravityScale = 0;
            player.transform.Translate(new Vector2(0, 1 * speed * Time.deltaTime), Space.World);
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

    private bool turn;

    // Start is called before the first frame update
    private void Start()
    {
        player = new CatPlayer(director, self, button, speed, jumpForce, false, false);
        turn = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Debug.DrawRay(transform.position, Vector2.right * 7.5f, Color.red);
            player.SetActive();
            if (player.active)
            {
                gameObject.layer = 8;
                player.Move();
                var rayHitR = Physics2D.Raycast(transform.position, Vector2.right, 7.5f, LayerMask.GetMask("Ground"));
                var rayHitL = Physics2D.Raycast(transform.position, Vector2.left, 7.5f, LayerMask.GetMask("Ground"));
                if (rayHitL.collider != null || rayHitR.collider != null)
                {
                    if (player.up && player.isJump) player.climb = true;
                }
                else
                {
                    player.climb = false;
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
                }
                else
                {
                    turn = false;
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