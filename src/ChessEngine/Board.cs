namespace ChessEngine;
using System;

public class Board
{
    public ulong[,] Pieces = new ulong[2, 6];
    public ulong WhiteOccupancy => GetOccupancy(Color.White);
    public ulong BlackOccupancy => GetOccupancy(Color.Black);
    public ulong AllOccupancy => WhiteOccupancy | BlackOccupancy;

    public Color SideToMove = Color.White;

    public LoadBoard()
    {
        ClearBitboards();
        LoadFEN();
    }

    public ulong GetOccupancy(Color color)
    {
        ulong occ = 0;
        for (int i = 0; i < 6; i++;)
        {
            occ |= Pieces[(int)color, i];
        }
        return occ;
    }

    void ClearBitboards()
    {
        for (int color = 0; color < 2; color++)
            for (int piece = 0; piece < 6; piece++)
                Pieces[color, piece] = 0UL;
    }

    void LoadFEN(string fen)
    {
        int pointer = 63;
        foreach (char c in fen)
        {
            if (c == ' ') break;

            if (char.IsDigit(c))
            {
                pointer -= c - '0';
            }
            else if (char.IsLetter(c))
            {
                bool isWhite = char.IsUpper(c);
                char piece = char.ToLower(c);

                int color = isWhite ? 0 : 1;

                int type = piece switch
                {
                    'p' => (int)Piece.Pawn,
                    'n' => (int)Piece.Knight,
                    'b' => (int)Piece.Bishop,
                    'r' => (int)Piece.Rook,
                    'q' => (int)Piece.Queen,
                    'k' => (int)Piece.King,
                    _ => -1
                };

                if (type >= 0)
                {
                    Pieces[color, type] = Bitboard.SetBit(Pieces[color, type], pointer);
                }
                pointer--;
            }
        }
    }
}