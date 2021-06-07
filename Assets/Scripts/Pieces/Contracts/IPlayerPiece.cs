using UnityEngine;

namespace LaserChess.Pieces.Contracts
{
    public interface IPlayerPiece
    {
        void HighlightMovementSpots();

        void HighlightCombatSpots();

        void Move(int row, int col);

        void Attack(Vector2 markerPos);

        void Reset();
    }
}