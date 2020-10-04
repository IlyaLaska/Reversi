using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public IPlayer playerWhite;
    public IPlayer playerBlack;
    public Game gameO;
    public BoardUpdate boardUpdateO;
    public BoardSquareProperties boardSquarePropertiesO;
    bool escPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBlack = createPlayer(PlayerEnum.black, GameProperties.playerBlackIsHuman);
        playerWhite = createPlayer(PlayerEnum.white, GameProperties.playerWhiteIsHuman);
        gameO = new Game(playerBlack, playerWhite);
        boardUpdateO = GameObject.FindObjectOfType<BoardUpdate>();
        boardUpdateO.setBoard(gameO.gameBoard);
        boardUpdateO.game = this.gameO;
        boardSquarePropertiesO = GameObject.FindObjectOfType<BoardSquareProperties>();
        gameO.initGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
           if(escPressed)
           {
                SceneManager.LoadScene("MainMenu");
           } else
            {
                escPressed = true;
            }
        }
    }

    private void OnEnable()
    {
        Game.gameEnded += gameEndHandler;
    }

    private void OnDisable()
    {
        Game.gameEnded -= gameEndHandler;
    }

    IPlayer createPlayer(PlayerEnum color, bool isHuman)
    {
        if(isHuman)
            return new HumanPlayer(color);
        else return new CPUPlayer(color);
    }

    public void gameEndHandler()
    {
        GameProperties.blackScore = playerBlack.score;
        GameProperties.whiteScore = playerWhite.score;
        SceneManager.LoadScene("MainMenu");
    }
    
}

