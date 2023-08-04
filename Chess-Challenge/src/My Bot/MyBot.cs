using ChessChallenge.API;

using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Console.WriteLine(board.GetPieceList(PieceType.Bishop, true).Count);

        return moves[0];
    }


    // Max is good for Bot
    // Min is good for Opp

    // TODO: Figure out return type for MiniMax
    public int MiniMax(Board board, int depth, bool MaxPlayer) // MaxPlayer is our bot
    {
        // If we have reached our depth or checkmate then we eval
        if (depth == 0 || board.IsInCheckmate()) // TODO: add game is over 
        {
            // Evaluate Position

            

            // Return Eval
            return 0;
        }

        // Bot's turn
        if (MaxPlayer)
        {
            int maxEval = int.MinValue;
            foreach (Move Move in board.GetLegalMoves())
            {
                // Make the move
                board.MakeMove(Move);
                // Eval the move
                maxEval = System.Math.Max(maxEval, MiniMax(board, depth - 1, false));
                // Undo Move
                board.UndoMove(Move);
            }


            // Return final eval
            return maxEval;
        }

        // Opp's turn
        else
        {
            int minEval = int.MaxValue;
            foreach (Move Move in board.GetLegalMoves())
            {
                // Make the move
                board.MakeMove(Move);
                // Eval the move
                minEval = System.Math.Min(minEval, MiniMax(board, depth - 1, true));
                // Undo Move
                board.UndoMove(Move);
            }


            // Return Eval
            return minEval;
        }
        
        
        return -1;
    }

}