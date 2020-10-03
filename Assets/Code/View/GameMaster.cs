using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Transform boardO;
    public IPlayer playerWhite;
    public IPlayer playerBlack;
    public Game gameO;
    // Start is called before the first frame update
    void Start()
    {
        instantiateBoard();
        playerWhite = new HumanPlayer();
        playerBlack = new HumanPlayer();
        gameO = new Game(playerWhite, playerBlack);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void instantiateBoard()
    {
        for (float y = -3.5f; y < 4.5f; y++)
        {
            for (float x = -3.5f; x < 4.5f; x++)
            {
                Instantiate(boardO, new Vector2(x, y), boardO.rotation);
            }
        }
        Debug.Log(boardO.tag);
    }
}
