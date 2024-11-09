using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButterflyMove : MonoBehaviour
{
    public float speed;
    public float radius;
    private Vector2 path;
    private float angle;
    private Vector3 center;
    public int direc;
    // Start is called before the first frame update
    void Start()
    {
        center.x = transform.position.x - direc*radius;
        center.y = transform.position.y;
        path.x = 0;
        path.y = 0;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (angle >= 2 * Mathf.PI)
        {
            angle = 0;
        }
        else
        {
            angle += speed * Time.deltaTime;
        }
        path.x = direc*radius*Mathf.Cos(angle)+center.x;
        path.y = direc*radius*Mathf.Sin(angle)+center.y;
        transform.position = path;
    }
}
