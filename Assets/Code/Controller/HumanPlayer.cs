using UnityEngine;

public class HumanPlayer : IPlayer
{
    public HumanPlayer(PlayerEnum color)
    {
        this.color = color;
        this.score = 0;
    }
    public int score { get; set; }
    public PlayerEnum color { get; set; }
    public int[] currentTurnCoords { get; set; }
    public int[] getMove() { return this.currentTurnCoords; }
}
