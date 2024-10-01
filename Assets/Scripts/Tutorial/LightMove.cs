using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    private SceneDirector sDirector;
    public Vector4 direc;
    public float x, y;
    public bool move=false;
    public Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        sDirector = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        sDirector.lightMove = move;
        if (Time.timeScale != 0 && move)
        {
            x -= 0.5f * (direc.w / 100f);
            y = x * x * direc.x + direc.y * x + direc.z;
            transform.position = new Vector2(x, y);
            if (x <= pos.x - direc.w)
            {
                move = false;
                Debug.Log("arrive");
                sDirector.lightMove = false;
                Destroy(gameObject);
            }
        }
    }
}
