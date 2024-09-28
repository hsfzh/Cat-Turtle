using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject slime, cat, turtle;
    public Vector3 slimePos, catPos, turtlePos;
    [SerializeField] private GameObject[] row1, row2, row3, row4, row5, row6;
    public int rowSize = 6;
    public int columnSize = 8;
    public float tileSize;
    public GameObject slimeBtn;
    public GameObject catBtn;
    public GameObject turtleBtn;
    private Vector3 clickPosition;
    public Vector3[,] tilePositions;
    public GameObject[,] tiles;
    private List<Vector2> playerTiles;
    public Vector2 playerStartTile;
    public GameObject[] lights;
    public int curLights;
    private int onLights;
    private int maxLights = 11;

    // Start is called before the first frame update
    private void Start()
    {
        slime.SetActive(true);
        cat.SetActive(true);
        turtle.SetActive(true);
        slime.transform.position = slimePos;
        cat.transform.position = catPos;
        turtle.transform.position = turtlePos;
        curLights = 1;
        onLights = 1;
        lights[0].SetActive(false);
        for (int i = 1; i < maxLights; i++)
        {
            lights[i].SetActive(true);
            lights[i].GetComponent<Image>().color = new Color(0, 1, 1);
        }
        tileSize = 120f / columnSize;
        playerTiles = new List<Vector2>();
        playerTiles.Clear();
        playerTiles.Add(playerStartTile);
        slimeBtn.SetActive(false);
        catBtn.SetActive(false);
        turtleBtn.SetActive(false);
        player = slime;
        tiles = new GameObject [rowSize, columnSize];
        for (var i = 0; i < rowSize; i++)
        for (var j = 0; j < columnSize; j++)
            switch (i + 1)
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
            }

        tilePositions = new Vector3[rowSize, columnSize];
        for (var i = 0; i < rowSize; i++)
        for (var j = 0; j < columnSize; j++)
        {
            if (i == 4 && j == 0)
                tiles[i, j].SetActive(false);
            else
                tiles[i, j].SetActive(true);
            tilePositions[i, j] = tiles[i, j].transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (player != slime)
            {
                slimeBtn.SetActive(true);
                catBtn.SetActive(false);
                turtleBtn.SetActive(false);
            }
            else if (Mathf.Abs((slime.transform.position - cat.transform.position).magnitude) <= 2)
            {
                catBtn.SetActive(true);
            }
            else if (Mathf.Abs((slime.transform.position - turtle.transform.position).magnitude) <= 2)
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
                var x = false;
                var y = false;
                var z = false;
                for (var i = 0; i < rowSize; i++)
                for (var j = 0; j < columnSize; j++)
                {
                    x = clickPosition.x > tilePositions[i, j].x - tileSize / 2 &&
                        clickPosition.x < tilePositions[i, j].x + tileSize / 2;
                    y = clickPosition.y > tilePositions[i, j].y - tileSize / 2 &&
                        clickPosition.y < tilePositions[i, j].y + tileSize / 2;
                    z = !(player.transform.position.x > tilePositions[i, j].x - tileSize / 2 &&
                          player.transform.position.x < tilePositions[i, j].x + tileSize / 2 &&
                          player.transform.position.y > tilePositions[i, j].y - tileSize / 2 &&
                          player.transform.position.y < tilePositions[i, j].y + tileSize / 2);
                    if (x && y && z)
                    {
                        Vector2 tile = new Vector2(i, j);
                        if (tiles[i, j].activeSelf && curLights<maxLights)
                        {
                            tiles[i, j].SetActive(false);
                            lights[curLights].GetComponent<Image>().color = new Color(0, 0, 0);
                            curLights += 1;
                        }
                        else if(!playerTiles.Contains(tile) && curLights<=maxLights && !tiles[i,j].activeSelf)
                        {
                            tiles[i, j].SetActive(true);
                            curLights -= 1;
                            lights[curLights].GetComponent<Image>().color = new Color(0, 1, 1);
                            
                        }
                        break;
                    }
                }
            }
            if(onLights<maxLights)
                CheckLights();
        }
    }
    public void CheckLights()
    {
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                bool x = (player.transform.position.x > tilePositions[i, j].x - tileSize / 2 &&
                          player.transform.position.x < tilePositions[i, j].x + tileSize / 2 &&
                          player.transform.position.y > tilePositions[i, j].y - tileSize / 2 &&
                          player.transform.position.y < tilePositions[i, j].y + tileSize / 2);
                if (x)
                {
                    Vector2 tile = new Vector2(i, j);
                    if (!playerTiles.Contains(tile))
                    {
                        playerTiles.Add(tile);
                        lights[onLights].SetActive(false);
                        onLights += 1;
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

    public void Reset()
    {
        Start();
    }
}