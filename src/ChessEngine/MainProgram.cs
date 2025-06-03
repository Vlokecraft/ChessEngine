namespace ChessEngine;

public class MainProgram
{
    static void Main()
    {
        Board board = new Board("3N4/b1p2P2/p2n4/4p3/2k1P3/1nP3qR/1K1P4/4R3 w - - 0 1");

        ulong moves = MoveGenerator.GenerateWhitePawnPushes(board);
        PrintBitboard(moves);
        board.Display();

        Console.WriteLine("Generating all magic data...");
        var magicData = MagicMaker.GenerateAllMagicData();
        Console.WriteLine("Magic data generated. Saving to file...");
        string filePath = "magic_data.json";
        MagicMaker.SaveMagicDataToFile(filePath, magicData);
        Console.WriteLine("Program finished. Check the output file.");
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