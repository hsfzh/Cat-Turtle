using UnityEngine;

public class CatController : MonoBehaviour
{
    public GameObject director;
    public GameObject self;
    public GameObject button;
    public float speed;
    public float jumpForce;

    private Player player;

    // Start is called before the first frame update
    private void Start()
    {
        player = new Player(director, self, button, speed, jumpForce, false);
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
                    Debug.Log("벽");
                    if (player.up) player.climb = true;
                }
                else
                {
                    player.climb = false;
                }

                if (!player.climb) player.rigid.gravityScale = 4.5f;
                //transform.localRotation = Quaternion.Euler(0,0,0);
                /*else
                {
                    if (rayHitL.collider != null)
                        transform.localRotation = Quaternion.Euler(0,0,-90);
                    if(rayHitR.collider != null)
                        transform.localRotation = Quaternion.Euler(0,0,90);
                }*/
            }
            else
            {
                gameObject.layer = 7;
            }
        }
    }
}