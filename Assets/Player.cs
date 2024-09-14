using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private float speed;
    private float jumpForce;
    public int direction;
    private float facing;
    public GameObject button;
    private bool left, right, up;
    private Rigidbody2D rigid;
    private bool isJump;
    private GameObject player;
    public bool active;
    private GameObject director;
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
        isJump = false;
        speed = s;
        rigid = player.GetComponent<Rigidbody2D>();
        jumpForce = j;
        active = a;
    }
    private void DetectGround()
    {
        RaycastHit2D rayHit =
            Physics2D.Raycast(rigid.position, new Vector2(0, -1), 3.75f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            isJump = false;
        }
    }
    public void Move()
    {
        DetectGround();
        left = button.GetComponent<ButtonController>().left;
        right = button.GetComponent<ButtonController>().right;
        up = button.GetComponent<ButtonController>().up;
        if (left||Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
        if (right||Input.GetKey(KeyCode.D))
        {
            direction = 1;
        }
        if ((up||Input.GetKeyDown(KeyCode.W)) && isJump==false)
        {
            isJump = true;
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }
        if(!left&&!right)
        {
            direction = 0;
        }
        player.transform.Translate(new Vector2(direction,0)*speed*Time.deltaTime);
        if (direction != 0)
        {
            facing = direction;
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
