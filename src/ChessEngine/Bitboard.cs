namespace ChessEngine;
using System.Numerics;

public static class Bitboard
{
    public static ulong SetBit(ulong board, int square) => board | (1UL << square);
    public static ulong ClearBit(ulong board, int square) => board & ~(1UL << square);
    public static bool GetBit(ulong board, int square) => (board & (1UL << square)) != 0;

    public static int PopCount(ulong bb) => BitOperations.PopCount(bb);
    public static int LSB(ulong bb) => BitOperations.TrailingZeroCount(bb);
}