using System;

public class CPUPlayer : IPlayer
{
    public int score { get; set; }
    public PlayerEnum color { get; set; }
    public int[] currentTurnCoords { get; set; }

    public int[] getMove(int[][] validMoves)
    {
        System.Random random = new System.Random();
        int move = random.Next(0, validMoves.Length);
        return validMoves[move];
    }
}
