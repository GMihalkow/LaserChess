namespace LaserChess.Pieces.Contracts
{
    public interface IPlayerPiece
    {
        void HighlightAvailableSpots();

        void Move(int row, int col);
    }
}