using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shiny_Control : MonoBehaviour
{
    public int type; //1=start
    public SpriteRenderer sprite;
    private float speed;
    private float alpha;
    private float delta;
    // Start is called before the first frame update
    void Start()
    {
        delta = 0;
        if (type==1)
        {
            speed = 204/0.7f;
            alpha = 255;
        }
        else
        {
            speed = Random.Range(204/1.3f, 204/0.8f);
            alpha = Random.Range(51, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        alpha = Mathf.PingPong(delta * speed, 204) + 51;
        sprite.color = new Color(1,1,1, alpha/255);
    }
}
