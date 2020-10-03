using UnityEngine;

public class HumanPlayer : IPlayer
{
    public int score { get; set; }
    public PlayerEnum color { get; set; }


    public int[] getMove(int[][] validMoves)
    {
        //AAAAAAAAAAAA
        return new int[] { 1, 1 };
    }

}
