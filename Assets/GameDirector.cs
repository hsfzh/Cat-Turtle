using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject [] tiles;
    private Vector3 [] tilePositions;
    private Vector3 clickPosition;
    private int size;
    // Start is called before the first frame update
    void Start()
    {
        size = tiles.Length;
        tilePositions = new Vector3[size];
        for(int i=0; i<size; i++)
        {
            tiles[i].SetActive(true);
            tilePositions[i] = tiles[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            Debug.Log(clickPosition);
            bool x = false;
            bool y = false;
            for (int i = 0; i < size; i++)
            {
                x = (clickPosition.x > tilePositions[i].x - 0.75 && clickPosition.x < tilePositions[i].x + 0.75);
                y = (clickPosition.y > tilePositions[i].y - 0.75 && clickPosition.y < tilePositions[i].y + 0.75);
                if (x && y)
                {
                    if (tiles[i].activeSelf)
                    {
                        tiles[i].SetActive(false);
                    }
                    else
                    {
                        tiles[i].SetActive(true);
                    }
                    break;
                }
            }
        }
    }
}
