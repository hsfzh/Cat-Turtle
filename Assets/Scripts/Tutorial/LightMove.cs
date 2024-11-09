using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    private SceneDirector sDirector;
    public Vector3[] direc;
    private Vector3 path;
    private Vector3 dist;
    public float x, y;
    public bool move=false;
    public Vector3 pos;
    public GameObject tile;
    
    // Start is called before the first frame update
    void Start()
    {
        sDirector = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
        path = direc[0];
        dist=direc[1];
    }

    // Update is called once per frame
    void Update()
    {
        sDirector.lightMove = move;
        if (Time.timeScale != 0 && move)
        {
            x -= (100f*Time.deltaTime)*dist.x/100f;
            y = x * x * path.x + path.y * x + path.z;
            transform.position = new Vector2(x, y);
            if (x <= pos.x - dist.x)
            {
                move = false;
                sDirector.lightMove = false;
                if (tile != null)
                {
                    tile.SetActive(false);
                }
                Destroy(gameObject);
            }
        }
    }
}
