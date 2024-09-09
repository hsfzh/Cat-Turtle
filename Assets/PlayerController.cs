using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject director;
    private GameObject[,] tiles;
    private Vector3[,] tilePositions;
    private GameObject[] playerDirection=new GameObject[3]; //앞, 뒤, 위, 아래 순 & 직전 타일은 포함 X
    private Vector3 playerPos;
    private Vector2 playerTilePos;
    private Vector2 playerPrevTilePos;
    private int tileRowSize;
    private int tileColumnSize;
    
    // Start is called before the first frame update
    void Start()
    {
        tileRowSize = director.GetComponent<GameDirector>().rowSize;
        tileColumnSize = director.GetComponent<GameDirector>().columnSize;
        playerPrevTilePos = new Vector2(0, 0);
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
                x = (playerPos.x > tilePositions[i,j].x - 0.75 && playerPos.x < tilePositions[i,j].x + 0.75);
                y = (playerPos.y > tilePositions[i,j].y - 0.75 && playerPos.y < tilePositions[i,j].y + 0.75);
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
        playerDirection[0] = tiles[row, column + 1];
        playerDirection[1] = tiles[row - 1, column];
        playerDirection[2] = tiles[row + 1, column];
        playerPrevTilePos = playerTilePos;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position;
        for (int i = 0; i < playerDirection.Length; i++)
        {
            if (!playerDirection[i].activeSelf)
            {
                transform.Translate(new Vector3(1.5f*Time.deltaTime, 0));
                
            }
        }
    }

    void DirectionChoice(Vector2 pos, Vector2 prevPos)
    {
        int row = (int)pos.x;
        int column = (int)pos.y;
        GameObject a = tiles[row, column + 1];
        GameObject b = tiles[row, column - 1];
        GameObject c = tiles[row - 1, column];
        GameObject d = tiles[row + 1, column];
        if (a == tiles[(int)prevPos.x, (int)prevPos.y])
        {
            playerDirection[0] = b;
            playerDirection[1] = c;
            playerDirection[2] = d;
        }
        else if (b == tiles[(int)prevPos.x, (int)prevPos.y])
        {
            playerDirection[0] = a;
            playerDirection[1] = c;
            playerDirection[2] = d;
        }
        else if (c == tiles[(int)prevPos.x, (int)prevPos.y])
        {
            playerDirection[0] = a;
            playerDirection[1] = b;
            playerDirection[2] = d;
        }
        else if (d == tiles[(int)prevPos.x, (int)prevPos.y])
        {
            playerDirection[0] = a;
            playerDirection[1] = b;
            playerDirection[2] = c;
        }
    }
}
