using ChessChallenge.API;

using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        return MiniMax(board, 3, true).bestMove;
    }


    // Max is good for Bot
    // Min is good for Opp

    // TODO: Figure out return type for MiniMax
    public (int eval, Move bestMove) MiniMax(Board board, int depth, bool MaxPlayer) // MaxPlayer is our bot
    {
        // If we have reached our depth or checkmate then we eval
        if (depth == 0 || board.IsInCheckmate()) // TODO: add game is over 
        {
            // Evaluate Position
            Random rand = new Random();
            // Return Eval
            return (rand.Next(), Move.NullMove); // Do I need to return the right move?
        }

        // Bot's turn
        if (MaxPlayer)
        {
            int maxEval = int.MinValue;
            Move BestMaxMove = Move.NullMove;
            foreach (Move Move in board.GetLegalMoves())
            {
                // Make the move
                board.MakeMove(Move);
                // Eval the move
                int currentEval = MiniMax(board, depth - 1, false).eval;
                if (currentEval > maxEval)
                {
                    maxEval = currentEval;
                    BestMaxMove = Move;
                }
                //maxEval = System.Math.Max(maxEval, MiniMax(board, depth - 1, false).eval);
                // Undo Move
                board.UndoMove(Move);
            }


            // Return final eval
            return (maxEval, BestMaxMove);
        }

        // Opp's turn
        else
        {
            int minEval = int.MaxValue;
            Move BestMinMove = Move.NullMove;
            foreach (Move Move in board.GetLegalMoves())
            {
                // Make the move
                board.MakeMove(Move);
                // Eval the move
                int currentEval = MiniMax(board, depth - 1, false).eval;
                if (currentEval < minEval)
                {
                    minEval = currentEval;
                    BestMinMove = Move;
                }
                // Undo Move
                board.UndoMove(Move);
            }


            // Return Eval
            return (minEval, BestMinMove);
        }
    }

}