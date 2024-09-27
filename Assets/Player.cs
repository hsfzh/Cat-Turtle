using UnityEngine;

public class Player
{
    public GameObject director;
    public readonly float jumpForce;
    public readonly GameObject player;

    public float speed;
    public bool active;
    public GameObject button;
    public int direction;
    public float facing;
    public bool isJump;
    public bool left, right, up, down;
    public Rigidbody2D rigid;

    public Player()
    {
        
    }
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
        down = false;
        isJump = false;
        speed = s;
        rigid = player.GetComponent<Rigidbody2D>();
        jumpForce = j;
        active = a;
    }

    private void DetectGround()
    {
        var rayHit =
            Physics2D.Raycast(rigid.position, new Vector2(0, -1), 3.75f, LayerMask.GetMask("Ground", "Tile"));
        if (rayHit.collider != null)
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }
    }

    public virtual void Move()
    {
        DetectGround();
        if (Input.GetKey(KeyCode.A)) button.GetComponent<ButtonController>().left = true;
        if (Input.GetKey(KeyCode.D)) button.GetComponent<ButtonController>().right = true;
        if (Input.GetKey(KeyCode.W)) button.GetComponent<ButtonController>().up = true;
        if (Input.GetKey(KeyCode.S)) button.GetComponent<ButtonController>().down = true;
        left = button.GetComponent<ButtonController>().left;
        right = button.GetComponent<ButtonController>().right;
        up = button.GetComponent<ButtonController>().up;
        down = button.GetComponent<ButtonController>().down;
        if (left) direction = -1;
        if (right) direction = 1;
        if ((up || Input.GetKeyDown(KeyCode.W)) && isJump == false)
        {
            isJump = true;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.A)) button.GetComponent<ButtonController>().left = false;
        if (Input.GetKeyUp(KeyCode.D)) button.GetComponent<ButtonController>().right = false;
        if (Input.GetKeyUp(KeyCode.W)) button.GetComponent<ButtonController>().up = false;
        if (Input.GetKeyUp(KeyCode.S)) button.GetComponent<ButtonController>().down = false;
        if (!left && !right) direction = 0;
        player.transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0), Space.World);
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