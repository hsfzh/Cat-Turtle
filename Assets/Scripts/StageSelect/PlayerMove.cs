using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoints;
    public int curPoint;
    public int[] stagePoints;
    public int stage;
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
        transform.position=new Vector3(-34.24f, -64.67f, 0);
        curPoint = stage = -1;
        move = false;
    }

    void Update()
    {
        if (move)
        {
            if (curPoint < stagePoints[stage])
            {
                Vector3 direc = wayPoints[curPoint + 1].position - transform.position;
                transform.Translate(direc.normalized*speed*Time.deltaTime);
                if (Vector3.Distance(transform.position, wayPoints[curPoint + 1].position) < 0.1f)
                {
                    curPoint += 1;
                }
                if (transform.position.x < wayPoints[curPoint+1].position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX=false;
                } else if (transform.position.x > wayPoints[curPoint+1].position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX=true;
                }
            }
            else if (curPoint > stagePoints[stage])
            {
                Vector3 direc = wayPoints[curPoint - 1].position - transform.position;
                transform.Translate(direc.normalized*speed*Time.deltaTime);
                if (Vector3.Distance(transform.position, wayPoints[curPoint - 1].position) < 0.1f)
                {
                    curPoint -= 1;
                }
                if (transform.position.x < wayPoints[curPoint-1].position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX=false;
                } else if (transform.position.x > wayPoints[curPoint-1].position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipX=true;
                }
            }
            
            if (curPoint == stagePoints[stage])
                move = false;
        }
        
    }
    public void Move(int stage)
    {
        this.stage = stage;
        move=true;
    }
}
