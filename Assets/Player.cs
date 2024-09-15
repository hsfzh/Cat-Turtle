using UnityEngine;

public class Player
{
    private readonly GameObject director;
    private readonly float jumpForce;
    private readonly GameObject player;

    private readonly float speed;
    public bool active;
    public GameObject button;
    public int direction;
    private float facing;
    public bool isJump;
    public bool left, right, up, climb;
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    public Player(GameObject d, GameObject p, GameObject b, float s, float j, bool a)
    {
        director = d;
        player = p;
        button = b;
        direction = 0;
        facing = 1;
        left = false;
        right = false;
        up = false;
        climb = false;
        isJump = false;
        speed = s;
        rigid = player.GetComponent<Rigidbody2D>();
        jumpForce = j;
        active = a;
    }

    private void DetectGround()
    {
        var rayHit =
            Physics2D.Raycast(rigid.position, new Vector2(0, -1), 3.75f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null) isJump = false;
    }

    public void Move()
    {
        DetectGround();
        if (Input.GetKey(KeyCode.A)) button.GetComponent<ButtonController>().left = true;
        if (Input.GetKey(KeyCode.D)) left = button.GetComponent<ButtonController>().right = true;
        if (Input.GetKey(KeyCode.W)) left = button.GetComponent<ButtonController>().up = true;
        left = button.GetComponent<ButtonController>().left;
        right = button.GetComponent<ButtonController>().right;
        up = button.GetComponent<ButtonController>().up;
        if (left) direction = -1;
        if (right) direction = 1;
        if ((up || Input.GetKeyDown(KeyCode.W)) && isJump == false)
        {
            isJump = true;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (climb && up)
        {
            rigid.gravityScale = 0;
            player.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.A)) button.GetComponent<ButtonController>().left = false;
        if (Input.GetKeyUp(KeyCode.D)) left = button.GetComponent<ButtonController>().right = false;
        if (Input.GetKeyUp(KeyCode.W)) left = button.GetComponent<ButtonController>().up = false;
        if (!left && !right) direction = 0;
        player.transform.Translate(new Vector2(direction, 0) * speed * Time.deltaTime);
        if (direction != 0) facing = direction;
    }

    public void SetActive()
    {
        if (director.GetComponent<GameDirector>().player == player)
            active = true;
        else
            active = false;
    }
}