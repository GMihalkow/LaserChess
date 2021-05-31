using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Grunt : Piece
    {
        public override void HighlightAvailableSpots()
        {
            if (this._isSelected) return;

            this._isSelected = true;

            var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);

            marker.GetComponent<Mover>().Move(this._mover.CurrentRow + 1, this._mover.CurrentCol);
        }

        public override void Move(int row, int col)
        {
            this._mover.Move(row, col);
        }
    }
}