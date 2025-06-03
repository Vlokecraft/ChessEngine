namespace ChessEngine;
using System;

public static class MoveGenerator
{
    public const int MaxMoves = 218;

    public static Move[] GenerateMoveList(Board board)
    {
        Span<Move> moveList = new Move[MaxMoves];
        int currMoveIndex = 0;
        ulong rooks = board.Pieces[(int)Color.White, (int)Piece.Rook];
        int rookPop = Bitboard.PopCount(rooks);
        for (int i = 0; i < rookPop; i++)
        {
            int rookToTest = Bitboard.LSB(rooks);
            GenerateRookMoves(rookToTest, moveList);
            rooks = Bitboard.PopBit(rooks, rookToTest);
        }
    }

    public static ulong GenerateWhitePawnPushes(Board board)
    {
        ulong pawns = board.Pieces[(int)Color.White, (int)Piece.Pawn];
        ulong empty = ~board.AllOccupancy;

        ulong singlePush = (pawns << 8) & empty;

        ulong rank2 = 0x000000000000FF00;
        ulong doublePush = ((pawns & rank2) << 16) & empty & (empty << 8);

        return singlePush | doublePush;
    }

    public static ulong GenerateBlackPawnPushes(Board board)
    {
        ulong pawns = board.Pieces[(int)Color.Black, (int)Piece.Pawn];
        ulong empty = ~board.AllOccupancy;

        ulong singlePush = (pawns >> 8) & empty;

        ulong rank7 = 0x00FF000000000000;
        ulong doublePush = ((pawns & rank7) >> 16) & empty & (empty >> 8);

        return singlePush | doublePush;
    }

    public static ulong GenerateRookMoves(int startingSquare, Span<Move> moveList)
    {
        ulong rank = 0x00000000000000FF << (ulong)(8 * (startingSquare >> 3));
        ulong file = 0x0101010101010101 << (ulong)(startingSquare % 8);
        ulong rookMask = rank | file ^ (1UL << startingSquare);

        ulong blockers = rookMask & board.AllOccupancy;
        ulong key = (blockers * Magics.rookMagics[startingSquare]) >> Magics.rookShifts[startingSquare];

        ulong movesBitboard = rookMovesLookup[startingSquare][key];
        movesBitboard &= ~board.friendlyPiecesBitboard;


        while (movesBitboard != 0)
        {
            int targetSquare = Bitboard.LSB(movesBitboard);
            movesBitboard = Bitboard.PopBit(movesBitboard, targetSquare);

            moveList[currMoveIndex++] = new Move(startingSquare, targetSquare);
        }

    }
}