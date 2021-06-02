using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Grunt : PlayerPiece
    {
        public override void HighlightAvailableSpots()
        {
            if (this._isSelected) return;

            this._isSelected = true;

            for (int row = this._mover.CurrentRow - 1; row <= this._mover.CurrentRow + 1; row++)
            {
                if (row == this._mover.CurrentRow || row < 0 || row >= this._grid.Rows || !this._grid.IsPosEmpty(row, this._mover.CurrentCol)) continue;

                var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol);
            }

            for (int col = this._mover.CurrentCol - 1; col <= this._mover.CurrentCol + 1; col++)
            {
                if (col == this._mover.CurrentCol || col < 0 || col >= this._grid.Cols || !this._grid.IsPosEmpty(this._mover.CurrentRow, col)) continue;

                var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(this._mover.CurrentRow, col);
            }
        }
    }
}