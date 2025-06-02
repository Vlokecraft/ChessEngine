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
    public MoveType Type;
    public Piece? PromotionPiece;

    public Move(int from, int to, MoveType type = MoveType.Normal, Piece? promotionPiece = null)
    {
        From = from;
        To = to;
        Type = type;
        PromotionPiece = promotionPiece;
    }

    public override string ToString()
    {
        return $"{From}->{To} {Type}" + (PromotionPiece.HasValue ? $"={PromotionPiece}" : "");
    }
}