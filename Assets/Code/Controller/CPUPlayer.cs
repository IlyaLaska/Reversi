using System;

public class CPUPlayer : IPlayer
{
    public CPUPlayer(PlayerEnum color)
    {
        this.color = color;
        this.score = 0;
        this.isHuman = false;
    }
    public int score { get; set; }

    public bool isHuman { get; set; }
    public PlayerEnum color { get; set; }
    public int[] currentTurnCoords { get; set; }

    public int[] getMove(int[][] validMoves)
    {
        System.Random random = new System.Random();
        int move = random.Next(0, validMoves.Length);
        return new int[] { validMoves[move][0], validMoves[move][1]};
        //return new int[] { 0, 0 };
    }
}
