namespace ChessEngine;
using System;

public static class MoveGenerator
{
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
}