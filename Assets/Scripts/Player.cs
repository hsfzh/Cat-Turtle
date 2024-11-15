using System;
using UnityEngine;

public class Player:MonoBehaviour
{
    public SceneDirector sDirector;
    public GameObject director;
    public float jumpForce;
    public GameObject player;
    public GameObject[] lights;
    public int lightNum;

    public float speed;
    public bool active;
    public GameObject button;
    public int direction;
    public float facing;
    public bool isJump;
    public bool left, right, up, down;
    public Rigidbody2D rigid;
    public float height;
    
    public virtual void Initialize(SceneDirector s, GameObject d, GameObject p, GameObject[] l, GameObject b, float sp, float j, bool a, float h)
    {
        sDirector = s;
        director = d;
        player = p;
        lights = l;
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
        height = h;
    }

    private void DetectGround()
    {
        var rayHit =
            Physics2D.Raycast(rigid.position, new Vector2(0, -1), height, LayerMask.GetMask("Ground", "Tile"));
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
        Debug.DrawRay(player.transform.position, new Vector2(0, -1)*height, new Color(1,0,0));
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
            }
            if (!left && !right && !isJump)
            {
                direction = 0;
            }
            rigid.velocity = new Vector2(direction * speed, rigid.velocity.y);
            if (direction != 0) facing = direction;
        }
    }

    public void LightSpinning()
    {
        if (active)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
                lights[i].transform.localPosition =
                    new Vector3(
                        10 * Mathf.Cos(Mathf.PI / 2 + Mathf.PI * 2 / 3 * i + Time.time) + player.transform.position.x,
                        10 * Mathf.Sin(Mathf.PI / 2 + Mathf.PI * 2 / 3 * i + Time.time) + player.transform.position.y,
                        0);
            }
            if (lightNum == 2)
            {
                lights[2].SetActive(false);
            }
            if (lightNum == 1)
            {
                lights[2].SetActive(false);
                lights[1].SetActive(false);
            }
            if (lightNum == 0)
            {
                lights[2].SetActive(false);
                lights[1].SetActive(false);
                lights[0].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }

    public void LightOff()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (isJump)
        {
            direction = 0;
        }
    }
}