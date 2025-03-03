using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public SlimeController SlimeScript;
    public CatController CatScript;
    public TurtleController TurtleScript;
    public GameObject slime, cat, turtle;
    public Vector3 slimePos, catPos, turtlePos;
    public int rowSize;
    public int columnSize;
    public float tileSize;
    public float tileSizeX;
    public float tileSizeY;
    public GameObject slimeBtn;
    public GameObject catBtn;
    public GameObject turtleBtn;
    public GameObject slimeBtnF;
    public GameObject catBtnF;
    public GameObject turtleBtnF;
    private Vector3 clickPosition;
    public Vector3[,] tilePositions;
    public GameObject[,] tiles;
    public bool[,] tileClicked;
    private List<Vector2> playerTiles;
    public Vector2 playerStartTile;
    public List<GameObject> lights;
    private int curLights;
    private int onLights;
    public int maxLights;
    public GameObject tile, tileParent, lightPrefab, lightParent;

    // Start is called before the first frame update
    private void Start()
    {
        slime.SetActive(true);
        cat.SetActive(true);
        turtle.SetActive(true);
        slime.transform.position = slimePos;
        cat.transform.position = catPos;
        turtle.transform.position = turtlePos;
        curLights = 0;
        onLights = 0;
        tileSize = 120f / columnSize;
        tileSizeX = 1f / (float)columnSize;
        tileSizeY = 1f / (float)rowSize;
        playerTiles = new List<Vector2>();
        playerTiles.Clear();
        playerTiles.Add(playerStartTile);
        slimeBtn.SetActive(false);
        catBtn.SetActive(false);
        turtleBtn.SetActive(false);
        slimeBtnF.SetActive(true);
        catBtnF.SetActive(true);
        turtleBtnF.SetActive(true);
        player = slime;
        tiles = new GameObject [rowSize, columnSize];
        tilePositions = new Vector3[rowSize, columnSize];
        tileClicked = new bool[rowSize, columnSize];
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                tileClicked[i, j] = false;
            }
        }
        lights.Clear();
        MakeTile();
        MakeLight();
    }

    // Update is called once per frame
    private void Update()
    {
        if (SlimeScript == null)
        {
            SlimeScript = slime.GetComponent<SlimeController>();
        }
        if (CatScript == null)
        {
            CatScript = cat.GetComponent<CatController>();
        }
        if (TurtleScript == null)
        {
            TurtleScript = turtle.GetComponent<TurtleController>();
        }
        if (Time.timeScale != 0)
        {
            if (player != slime)
            {
                if (!TurtleScript.player.tunnel)
                {
                    slimeBtn.SetActive(true);
                    slimeBtnF.SetActive(false);
                }
                else
                {
                    slimeBtn.SetActive(false);
                    slimeBtnF.SetActive(true);
                }
                if (player == cat)
                {
                    if (Mathf.Abs((cat.transform.position - turtle.transform.position).magnitude) <= 6)
                    {
                        turtleBtn.SetActive(true);
                        turtleBtnF.SetActive(false);
                    }
                    else
                    {
                        turtleBtn.SetActive(false);
                        turtleBtnF.SetActive(true);
                    }
                    catBtn.SetActive(false);
                    catBtnF.SetActive(true);
                }else if (player == turtle)
                {
                    if (Mathf.Abs((turtle.transform.position - cat.transform.position).magnitude) <= 6)
                    {
                        catBtn.SetActive(true);
                        catBtnF.SetActive(false);
                    }
                    else
                    {
                        catBtn.SetActive(false);
                        catBtnF.SetActive(true);
                    }
                    turtleBtn.SetActive(false);
                    turtleBtnF.SetActive(true);
                }
            }
            else
            {
                slimeBtn.SetActive(false);
                slimeBtnF.SetActive(true);
                if (Mathf.Abs((slime.transform.position - cat.transform.position).magnitude) <= 6)
                {
                    catBtn.SetActive(true);
                    catBtnF.SetActive(false);
                }
                else
                {
                    catBtn.SetActive(false);
                    catBtnF.SetActive(true);
                }

                if (Mathf.Abs((slime.transform.position - turtle.transform.position).magnitude) <= 6)
                {
                    turtleBtn.SetActive(true);
                    turtleBtnF.SetActive(false);
                }
                else
                {
                    turtleBtn.SetActive(false);
                    turtleBtnF.SetActive(true);
                }
            }
            
            SlimeScript.player.lightNum = CatScript.player.lightNum = TurtleScript.player.lightNum = maxLights-curLights-1;
            CheckClick();
            if(onLights<maxLights-1)
                CheckLights();
        }
    }
    

    public void CheckClick()
    {
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
                    var tile = new Vector2(i, j);
                    if (tiles[i, j].activeSelf && curLights < maxLights - 1 && tileClicked[i,j]==false)
                    {
                        tileClicked[i, j] = true;
                        lights[curLights].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                        /*
                        Vector3[] lightPath=SetLightPath(lights[curLights].transform.position.x,
                            lights[curLights].transform.position.y, tilePositions[i,j].x, tilePositions[i,j].y);
                        GameObject l = Instantiate(lightPrefab);
                        LightMove lScript = l.GetComponent<LightMove>();
                        l.transform.position = lScript.pos = lights[curLights].transform.position;
                        lScript.tile = tiles[i, j];
                        lScript.x = lights[curLights].transform.position.x;
                        lScript.direc = lightPath;
                        lScript.move = true;
                        */
                        tiles[i, j].SetActive(false);
                        curLights += 1;
                    }
                    else if (!playerTiles.Contains(tile) && curLights <= maxLights - 1 && !tiles[i, j].activeSelf)
                    {
                        tileClicked[i, j] = false;
                        tiles[i, j].SetActive(true);
                        curLights -= 1;
                        lights[curLights].GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                    }
                    break;
                }
            }
        }
    }
    /*
    public Vector3[] SetLightPath(float a, float b, float c, float d)
    {
        float r = -Mathf.Abs((b-d)/(a*a-c*c+2*(a-c)*(5-a)));
        float p = 2f*r*(5-a);
        float q = -r*a*a-p*a+b;
        float distX = a - c;
        float distY = b - d;
        Vector3[] path = new Vector3[2];
        path[0] = new Vector3(r, p, q);
        path[1] = new Vector3(distX, distY, 0);
        return path;
    }
    */
    public void MakeTile()
    {
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
                if (i == playerStartTile.x && j == playerStartTile.y)
                {
                    tiles[i,j].SetActive(false);
                }
                else
                {
                    tiles[i,j].SetActive(true);
                }
            }
        }
    }

    public void MakeLight()
    {
        int lightColumn = 0;
        if (maxLights % 4 != 0)
        {
            lightColumn = maxLights / 4 + 1;
        }
        else
        {
            lightColumn = maxLights / 4;
        }
        for (int i = 0; i < lightColumn; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject l = CreateLight();
                float x = 1/5f;
                float y = 1/8f;
                l.transform.localScale = new Vector3(x, y, 0);
                Vector2 pos = new Vector2(-0.5f + x * (j + 0.5f)+0.2f*x*(j+1), 0.5f - y * (i + 0.5f)-0.2f*y*(i+1));
                l.transform.localPosition = pos;
                if (lights.Count<maxLights-1)
                {
                    lights.Add(l);
                }
                else
                {
                    Destroy(l);
                }
            }
        }
        lights.Reverse();
    }
    public GameObject CreateTile()
    {
        GameObject t = Instantiate(tile);
        t.transform.parent = tileParent.transform;
        return t;
    }

    public GameObject CreateLight()
    {
        GameObject l = Instantiate(lightPrefab);
        l.transform.parent = lightParent.transform;
        return l;
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
        if (player != cat)
        {
            SlimeScript.player.LightOff();
            player = cat;
            slime.SetActive(false);
        }
    }

    public void TurtleChange()
    {
        if (player != turtle)
        {
            SlimeScript.player.LightOff();
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
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                Destroy(tiles[i,j]);
            }
        }
        lights.Clear();
        Start();
    }
}