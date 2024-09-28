using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDirector : MonoBehaviour
{
    public Director d;
    public GameObject self;
    public int rowSize, columSize;
    public GameObject tile, tileBoard;
    public GameObject player;
    public GameObject[] players;
    public Vector3[] playerPos;
    public GameObject[] playerBtns;
    public Vector2 playerStartTile;
    public int maxLights;
    private Vector3 clickPosition;
    public GameObject light, lightBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        d = self.AddComponent<Director>();
        d.Initialize(player, players, playerPos, playerBtns, playerStartTile, maxLights, rowSize, columSize, tile, tileBoard, light, lightBoard);
    }
    
    // Update is called once per frame
    void Update()
    {
        d.CheckClick();
    }
}
