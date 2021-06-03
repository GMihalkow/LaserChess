namespace LaserChess.Pieces.Contracts
{
    public interface IPlayerPiece
    {
        void HighlightMovementSpots();

        void HighlightCombatSpots();

        void Move(int row, int col);
    }
}