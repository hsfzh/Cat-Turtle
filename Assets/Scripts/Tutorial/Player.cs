using UnityEngine;

public class Player:MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public float jumpForce;
    public GameObject player;

    public float speed;
    public bool active;
    public GameObject button;
    public int direction;
    public float facing;
    public bool isJump;
    public bool left, right, up, down;
    public Rigidbody2D rigid;
    
    public virtual void Initialize(SceneDirector s, GameObject d, GameObject p, GameObject b, float sp, float j, bool a)
    {
        sDirector = s;
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
        speed = sp;
        rigid = player.GetComponent<Rigidbody2D>();
        jumpForce = j;
        active = a;
    }

    private void DetectGround()
    {
        var rayHit =
            Physics2D.Raycast(rigid.position, new Vector2(0, -1), 7.5f, LayerMask.GetMask("Ground", "Tile"));
        if (rayHit.collider != null)
        {
            isJump = false;
        }
        else
        {
            isJump = true;
        }
    }

    public void DetectInput()
    {
        if (Input.GetKey(KeyCode.A)) button.GetComponent<ButtonController>().left = true;
        if (Input.GetKey(KeyCode.D)) button.GetComponent<ButtonController>().right = true;
        if (Input.GetKey(KeyCode.W)) button.GetComponent<ButtonController>().up = true;
        if (Input.GetKey(KeyCode.S)) button.GetComponent<ButtonController>().down = true;
        left = button.GetComponent<ButtonController>().left;
        right = button.GetComponent<ButtonController>().right;
        up = button.GetComponent<ButtonController>().up;
        down = button.GetComponent<ButtonController>().down;
        if (Input.GetKeyUp(KeyCode.A)) button.GetComponent<ButtonController>().left = false;
        if (Input.GetKeyUp(KeyCode.D)) button.GetComponent<ButtonController>().right = false;
        if (Input.GetKeyUp(KeyCode.W)) button.GetComponent<ButtonController>().up = false;
        if (Input.GetKeyUp(KeyCode.S)) button.GetComponent<ButtonController>().down = false;
    }
    public virtual void Move()
    {
        if (!sDirector.lightMove)
        {
            DetectGround();
            DetectInput();
            if (left) direction = -1;
            if (right) direction = 1;
            if (up && isJump == false)
            {
                isJump = true;
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            } if (!left && !right) direction = 0;
            player.transform.Translate(new Vector2(direction * speed * Time.deltaTime, 0), Space.World);
            if (direction != 0) facing = direction;
        }
    }

    public void SetActive()
    {
        if (director.GetComponent<GameDirector>().player == player)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }
}