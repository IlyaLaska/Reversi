using System;

public class Player : IPlayer
{
    public PlayerEnum color { get; set; }

    public Player(bool isWhite) {
        this.isWhite = isWhite;
    }
}