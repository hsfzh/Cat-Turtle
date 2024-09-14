using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    private Player player;
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        player = new Player(director, self, button, speed, jumpForce, false);
    }

    // Update is called once per frame
    void Update()
    {
        player.SetActive();
        if (player.active)
        {
            player.Move();
        }
    }
}
