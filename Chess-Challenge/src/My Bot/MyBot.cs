using ChessChallenge.API;

using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        return MiniMax(board, 3, true, board.IsWhiteToMove).bestMove;
    }

    public int BasicEval(Board board, bool isWhite)
    {
        int eval = 0;
        for (int i = 1; i < 7; i++)
        {
            eval += board.GetPieceList((PieceType)i, true).Count - board.GetPieceList((PieceType)i, false).Count;
        }
        if (!isWhite)
        {
            eval *= -1;
        }
        return eval;
    }
    
    
    
    
    // Max is good for Bot
    // Min is good for Opp

    // TODO: Figure out return type for MiniMax
    public (int eval, Move bestMove) MiniMax(Board board, int depth, bool MaxPlayer, bool isWhite) // MaxPlayer is our bot
    {
        // If we have reached our depth or checkmate then we eval
        if (depth == 0 || board.IsInCheckmate()) // TODO: add game is over 
        {
            // Evaluate Position
            // Return Eval
            return (BasicEval(board, isWhite), Move.NullMove); // Do I need to return the right move?
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
                int currentEval = MiniMax(board, depth - 1, false, isWhite).eval;
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
                int currentEval = MiniMax(board, depth - 1, false, isWhite).eval;
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