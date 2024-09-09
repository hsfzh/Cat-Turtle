using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject director;
    private GameObject[,] tiles;
    private Vector3[,] tilePositions;
    private Vector2[] playerDirection=new Vector2[3]; //앞, 뒤, 위, 아래 순 & 직전 타일은 포함 X
    private Vector3 playerPos;
    private Vector2 playerTilePos;
    private List<Vector2> playerPrevTilePos;
    private int tileRowSize;
    private int tileColumnSize;
    private float speed = 15f;
    public int direction;
    private float facing;
    public float tileSize = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        facing = 1;
        tileRowSize = director.GetComponent<GameDirector>().rowSize;
        tileColumnSize = director.GetComponent<GameDirector>().columnSize;
        tiles = new GameObject[tileRowSize, tileColumnSize];
        tilePositions = new Vector3[tileRowSize, tileColumnSize];
        for (int i = 0; i < tileRowSize; i++)
        {
            for (int j = 0; j < tileColumnSize; j++)
            {
                this.tiles[i,j]=director.GetComponent<GameDirector>().tiles[i, j];
                this.tilePositions[i, j] = director.GetComponent<GameDirector>().tilePositions[i, j];
            }
        }
        playerPos = transform.position;
        bool x = false;
        bool y = false;
        for(int i=0; i<tileRowSize; i++)
        {
            for (int j = 0; j < tileColumnSize; j++)
            {
                x = (playerPos.x > tilePositions[i,j].x - tileSize/2 && playerPos.x < tilePositions[i,j].x + tileSize/2);
                y = (playerPos.y > tilePositions[i,j].y - tileSize/2 && playerPos.y < tilePositions[i,j].y + tileSize/2);
                if (x && y)
                {
                    playerTilePos.x = i;
                    playerTilePos.y = j;
                    break;
                }
            }
        }
        int row = (int)playerTilePos.x;
        int column = (int)playerTilePos.y;
        playerDirection[0] = new Vector2(row, column + 1);
        playerDirection[1] = new Vector2(row - 1, column);
        playerDirection[2] = new Vector2(row + 1, column);
        playerPrevTilePos = new List<Vector2>();
        playerPrevTilePos.Add(playerTilePos);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerDirection.Length; i++)
        {
            if (tiles[(int)playerDirection[i].x, (int)playerDirection[i].y] != null)
            {
                if (!tiles[(int)playerDirection[i].x, (int)playerDirection[i].y].activeSelf)
                {
                    if (playerDirection[i].x == playerTilePos.x)
                    {
                        if (Mathf.Abs(transform.position.x - tilePositions[(int)playerDirection[i].x, (int)playerDirection[i].y].x) > 0.1f)
                        {
                            if(playerDirection[i].y-playerTilePos.y>0)
                            {
                                direction = 1;
                            }
                            else if (playerDirection[i].y - playerTilePos.y < 0)
                            {
                                direction = -1;
                            }
                            transform.Translate(new Vector2(speed,0)*Time.deltaTime*direction);
                        }
                        else
                        {
                            direction = 0;
                            playerTilePos.x = playerDirection[i].x;
                            playerTilePos.y = playerDirection[i].y;
                            DirectionChoice(playerTilePos);
                            playerPrevTilePos.Add(playerTilePos);
                        }
                    }
                    else if (playerDirection[i].y == playerTilePos.y)
                    {
                        if (Mathf.Abs(transform.position.y - tilePositions[(int)playerDirection[i].x, (int)playerDirection[i].y].y) > 0.1f)
                        {
                            if(playerDirection[i].x-playerTilePos.x>0)
                            {
                                direction = -1;
                            }
                            else if (playerDirection[i].x - playerTilePos.x < 0)
                            {
                                direction = 1;
                            }
                            transform.Translate(new Vector2(0, speed)*Time.deltaTime*direction);
                        }
                        else
                        {
                            direction = 0;
                            playerTilePos.x = playerDirection[i].x;
                            playerTilePos.y = playerDirection[i].y;
                            DirectionChoice(playerTilePos);
                            playerPrevTilePos.Add(playerTilePos);
                        }
                    }
                }
            }
        }
        if (direction != 0)
            facing = direction;
        transform.localScale = new Vector3(7.5f * facing, 7.5f, 1);
    }
    void DirectionChoice(Vector2 pos)
    {
        int row = (int)pos.x;
        int column = (int)pos.y;
        Vector2 a = new Vector2(row, column + 1);
        Vector2 b = new Vector2(row, column - 1);
        Vector2 c = new Vector2(row - 1, column);
        Vector2 d = new Vector2(row + 1, column);
        List<Vector2> validDirections= new List<Vector2>();
        if(a.y<tileColumnSize)
            validDirections.Add(a);
        if(b.y>=0)
            validDirections.Add(b);
        if(c.x>=0)
            validDirections.Add(c);
        if (d.x < tileRowSize)
            validDirections.Add(d);
        for (int i = 0; i < playerPrevTilePos.Count; i++)
        {
            if (validDirections.Contains(playerPrevTilePos[i]))
            {
                validDirections.Remove(playerPrevTilePos[i]);
            }
        }
        int size = validDirections.Count;
        Array.Resize(ref playerDirection, size);
        for (int i = 0; i < size; i++)
        {
            playerDirection[i] = validDirections[i];
        }
    }
}
