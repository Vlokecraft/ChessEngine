namespace ChessEngine;

public enum MoveType
{
    Normal,
    Promotion,
    Capture,
    PromotionCapture,
    EnPassant,
    Castle
}

public struct Move
{
    public int From;
    public int To;

    public Move(int from, int to)
    {
        From = from;
        To = to;
    }

    public override string ToString()
    {
        return $"{From}->{To} {Type}" + (PromotionPiece.HasValue ? $"={PromotionPiece}" : "");
    }
}