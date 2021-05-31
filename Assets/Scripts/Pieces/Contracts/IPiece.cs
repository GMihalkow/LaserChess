namespace LaserChess.Pieces.Contracts
{
    public interface IPiece
    {
        void HighlightAvailableSpots();

        void Move(int row, int col);
    }
}