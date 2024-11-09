using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shiny_Control : MonoBehaviour
{
    public Image img;
    public SpriteRenderer sprite;
    private float speed;
    private float delta;
    private float alpha;
    private int direc;
    // Start is called before the first frame update
    void Start()
    {
        if (sprite != null)
        {
            int s = Random.Range(8, 13);
            speed = (float)s / 100f;
            alpha = Random.Range(51, 256);
            direc = Random.Range(-1, 1) == 0 ? 1 : -1;
            delta = 255 - alpha;
        }

        if (img != null)
        {
            speed = 0.25f;
            alpha = 255;
            direc = -1;
            delta = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (direc == -1)
            delta += 0.1f;
        else
            delta = 0;
        if (delta > 205)
            delta = 0;
        if (direc==-1)
        {
            if (alpha > 51)
            {
                alpha = 255 - delta;
            }
            else
            {
                direc = 1;
            }
        }
        else if (direc==1)
        {
            if (alpha < 255)
            {
                alpha += 0.1f;
            }
            else
            {
                direc = -1;
            }
        }
        if(img!=null)
            img.color = new Color(1,1,1, alpha/255);
        if(sprite!=null)
            sprite.color = new Color(1,1,1, alpha/255);
    }
}
