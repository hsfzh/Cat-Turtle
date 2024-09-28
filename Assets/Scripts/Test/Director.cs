using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Director:MonoBehaviour
{
    public GameObject player;
    public GameObject[] players;
    public Vector3[] playerPos;
    public GameObject[] playerBtns;
    public List<Vector2> playerTiles;
    public Vector2 playerStartTile;
    public GameObject[,] lights;
    public int curLights;
    public int onLights;
    public int maxLights;
    
    public int rowSize;
    public int columnSize;
    public float tileSize;
    public float tileSizeX;
    public float tileSizeY;
    public Vector3[,] tilePositions;
    public GameObject[,] tiles;
    public GameObject tile;
    public GameObject tileParent;
    public GameObject light;
    public GameObject lightParent;
    private Vector3 clickPosition;

    public void Initialize(GameObject player, GameObject[] players, Vector3[] playerPos, GameObject[] playerBtns, Vector2 pStartTile, 
        int lightMax, int r, int c, GameObject tile, GameObject tp, GameObject light, GameObject lp)
    {
        this.player = player;
        this.players = players;
        this.playerPos = playerPos;
        this.playerBtns = playerBtns;
        playerStartTile = pStartTile;
        maxLights = lightMax;
        rowSize = r;
        columnSize = c;
        tileSize = 120f / (float)c;
        tileSizeX = 1f / (float)c;
        tileSizeY = 1f / (float)r;
        tiles = new GameObject[r, c];
        tilePositions = new Vector3[r, c];
        this.tile = tile;
        tileParent = tp;
        this.light = light;
        lightParent = lp;
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                GameObject t=CreateTile();
                t.transform.localScale = new Vector3(1 / (float)columnSize, 1 / (float)rowSize, 0);
                Vector2 pos = new Vector2(-0.5f+tileSizeX*(j+0.5f), 0.5f-tileSizeY*(i+0.5f));
                t.transform.localPosition = pos;
                tiles[i, j] = t;
                tilePositions[i, j] = tiles[i,j].transform.position;
            }
        }
        int lightColumn = 0;
        if (maxLights % 4 != 0)
        {
            lightColumn = maxLights / 4 + 1;
        }
        else
        {
            lightColumn = maxLights / 4;
        }
        lights = new GameObject[lightColumn, 4];
        for (int i = 0; i < lightColumn; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject l = CreateLight();
                l.transform.localScale = new Vector3(1 / (float)lightColumn, 1f / 4f, 0);
                lights[i, j] = l;
            }
        }
    }

    public GameObject CreateTile()
    {
        GameObject t=Instantiate(tile);
        t.transform.parent = tileParent.transform;
        return t;
    }

    public GameObject CreateLight()
    {
        GameObject l = Instantiate(light);
        l.transform.parent = lightParent.transform;
        return l;
    }

    public void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            Debug.Log(clickPosition);
            var x = false;
            var y = false;
            for (var i = 0; i < rowSize; i++)
            {
                for (var j = 0; j < columnSize; j++)
                {
                    x = clickPosition.x > tilePositions[i, j].x - tileSize / 2 &&
                        clickPosition.x < tilePositions[i, j].x + tileSize / 2;
                    y = clickPosition.y > tilePositions[i, j].y - tileSize / 2 &&
                        clickPosition.y < tilePositions[i, j].y + tileSize / 2;
                    if (x && y)
                    {
                        Vector2 tileClicked = new Vector2(i, j);
                        if (tiles[i, j].activeSelf)
                        {
                            tiles[i, j].SetActive(false);
                        }
                        else if(!tiles[i,j].activeSelf)
                        {
                            tiles[i, j].SetActive(true);
                        }
                        break;
                    }
                }
            }
        }
    }
}
