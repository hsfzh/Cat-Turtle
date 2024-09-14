using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject slime, cat, turtle;
    [SerializeField] private GameObject[] row1, row2, row3, row4, row5, row6;
    public GameObject [,] tiles;
    public Vector3 [,] tilePositions;
    private Vector3 clickPosition;
    public int rowSize=6;
    public int columnSize=8;
    public float tileSize = 15f;
    public GameObject slimeBtn;
    public GameObject catBtn;
    public GameObject turtleBtn;
    // Start is called before the first frame update
    void Start()
    {
        slimeBtn.SetActive(false);
        catBtn.SetActive(false);
        turtleBtn.SetActive(false);
        player = slime;
        tiles = new GameObject [rowSize, columnSize];
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                switch (i+1)
                {
                    case 1: 
                        tiles[i, j] = row1[j];
                        break;
                    case 2:
                        tiles[i, j] = row2[j];
                        break;
                    case 3:
                        tiles[i, j] = row3[j];
                        break;
                    case 4:
                        tiles[i, j] = row4[j];
                        break;
                    case 5:
                        tiles[i, j] = row5[j];
                        break;
                    case 6:
                        tiles[i, j] = row6[j];
                        break;
                    default:
                        break;
                }
            }
        }
        tilePositions = new Vector3[rowSize, columnSize];
        for(int i=0; i<rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                if (i == 4 && j == 0)
                {
                    tiles[i,j].SetActive(false);
                }
                else
                {
                    tiles[i,j].SetActive(true);
                }
                tilePositions[i,j] = tiles[i,j].transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != slime)
        {
            slimeBtn.SetActive(true);
        }else if (Mathf.Abs((slime.transform.position - cat.transform.position).magnitude) <= 2)
        {
            catBtn.SetActive(true);
        }else if (Mathf.Abs((slime.transform.position - turtle.transform.position).magnitude) <= 2)
        {
            turtleBtn.SetActive(true);
        }
        else
        {
            slimeBtn.SetActive(false);
            catBtn.SetActive(false);
            turtleBtn.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            bool x = false;
            bool y = false;
            bool z = false;
            for(int i=0; i<rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    x = (clickPosition.x > tilePositions[i, j].x - tileSize/2 &&
                         clickPosition.x < tilePositions[i, j].x + tileSize/2);
                    y = (clickPosition.y > tilePositions[i, j].y - tileSize/2 &&
                         clickPosition.y < tilePositions[i, j].y + tileSize/2);
                    z = !(player.transform.position.x > tilePositions[i, j].x - tileSize/2 &&
                          player.transform.position.x < tilePositions[i, j].x + tileSize/2 &&
                          player.transform.position.y > tilePositions[i, j].y - tileSize/2 &&
                          player.transform.position.y < tilePositions[i, j].y + tileSize/2);
                    if (x && y && z)
                    {
                        if (tiles[i,j].activeSelf)
                        {
                            tiles[i,j].SetActive(false);
                        }
                        else
                        {
                            tiles[i,j].SetActive(true);
                        }
                        break;
                    }
                }
            }
        }
    }
    public void CatChange()
    {
        if (player == slime)
        {
            player = cat;
            slime.SetActive(false);
        }
    }
    public void TurtleChange()
    {
        if (player == slime)
        {
            player = turtle;
            slime.SetActive(false);
        }
    }
    public void SlimeChange()
    {
        if (player != slime)
        {
            slime.transform.position = player.transform.position;
            player = slime;
            slime.SetActive(true);
        }
    }
}
