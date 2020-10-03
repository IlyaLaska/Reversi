using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameMaster gameMaster;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        BoardSquareProperties.BoardSquareClicked += boardSquareClickHandler;
    }

    private void OnDisable()
    {
        BoardSquareProperties.BoardSquareClicked -= boardSquareClickHandler;
    }

    public void boardSquareClickHandler(int xPos, int yPos)
    {
        //SEND TO MODEL
        //if (gameMaster.gameO.validMovesAndDirsForThisTurn.Length == 0)
        //{

        //}
    }
}
