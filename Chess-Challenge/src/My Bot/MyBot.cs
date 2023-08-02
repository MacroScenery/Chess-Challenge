using ChessChallenge.API;

using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        //Console.WriteLine("Test");
        return moves[0];
    }
}