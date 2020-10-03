using System;

public class CPUPlayer : IPlayer
{
    public int score { get; set; }
    public PlayerEnum color { get; set; }
    public int[] currentTurnCoords { get; set; }

    public int[] getMove()
    {
        int length = 8;
        System.Random random = new System.Random();
        int move = random.Next(0, length);
        //return validMoves[move];
        return new int[] { 0, 0 };
    }
}
