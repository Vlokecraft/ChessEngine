namespace ChessEngine;
using System;

public class Board
{
    public ulong[,] Pieces = new ulong[2, 6];
    public ulong WhiteOccupancy => GetOccupancy(Color.White);
    public ulong BlackOccupancy => GetOccupancy(Color.Black);
    public ulong AllOccupancy => WhiteOccupancy | BlackOccupancy;

    public Color SideToMove;

    public Board(string fen)
    {
        ClearBitboards();
        LoadFEN(fen);
    }

    public ulong GetOccupancy(Color color)
    {
        ulong occ = 0;
        for (int i = 0; i < 6; i++)
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
        string[] fenParts = fen.Split(' ');

        string piecePlacement = fenParts[0];
        int file = 0;
        int rank = 7;

        foreach (char c in piecePlacement)
        {
            if (c == '/')
            {
                rank--;
                file = 0;
                continue;
            }

            if (char.IsDigit(c))
            {
                file += c - '0';
            }
            else if (char.IsLetter(c))
            {
                bool isWhite = char.IsUpper(c);
                char piece = char.ToLower(c);

                int color = isWhite ? (int)Color.White : (int)Color.Black;

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
                    int squareIndex = rank * 8 + file;
                    Pieces[color, type] = Bitboard.SetBit(Pieces[color, type], squareIndex);
                }
                file++;
            }
        }
        if (fenParts.Length > 1) // Active Colour Check
        {
            char activeColorChar = fenParts[1][0];
            if (activeColorChar == 'w')
            {
                SideToMove = Color.White;
            }
            else if (activeColorChar == 'b')
            {
                SideToMove = Color.Black;
            }
        }
        // -- NOT YET IMPLEMENTED --
        // Castling Rights (state 2)
        // En Passant Targets (state 3)
        // Halfmove Clock (state 4)
        // Fullmove Number (state 5)
    }

    public void Display()
    {
        char[,] visualBoard = new char[8, 8];

        for (int rank = 7; rank >= 0; rank--)
        {
            Console.Write((rank+1)+" ");
            for (int file = 0; file < 8; file++)
            {
                int squareIndex = rank * 8 + file;
                bool pieceFound = false;

                for (int color = 0; color < 2; color++)
                {
                    for (int type = 0; type < 6; type++)
                    {
                        if (Bitboard.GetBit(Pieces[color, type], squareIndex))
                        {
                            char piece = type switch
                            {
                                0 => 'p',
                                1 => 'n',
                                2 => 'b',
                                3 => 'r',
                                4 => 'q',
                                5 => 'k'
                            };
                            bool colorBool = (color != 0);
                            visualBoard[rank, file] = (colorBool) ? char.ToUpper(piece) : char.ToLower(piece);

                            pieceFound = true;
                            break;
                        }
                    }
                    if (pieceFound)
                    {
                        break;
                    }
                }

                if (!pieceFound)
                {
                    visualBoard[rank, file] = '.'; // No piece found for this square, set to dot
                }

                Console.Write(visualBoard[rank, file] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }
}