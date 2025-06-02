namespace ChessEngine;

public class MainProgram
{
    static void Main()
    {
        Board board = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        ulong moves = MoveGenerator.GenerateBlackPawnMoves(board);
        PrintBitboard(moves);
    }

    public static void PrintBitboard(ulong bitboard)
    {
        for (int rank = 7; rank >= 0; rank--)
        {
            for (int file = 0; file < 8; file++)
            {
                int square = rank * 8 + file;
                ulong mask = 1UL << square;
                Console.Write((bitboard & mask) != 0 ? "1 " : ". ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}