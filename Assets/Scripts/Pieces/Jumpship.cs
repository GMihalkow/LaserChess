using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Jumpship : Piece
    {
        public override void HighlightAvailableSpots()
        {
            // TODO [GM]: extract code? (in Grunt too)
            if (this._isSelected) return;

            this._isSelected = true;

            for (int row = this._mover.CurrentRow - 2; row <= this._mover.CurrentRow + 2; row+=2)
            {
                var isNotInBounds = row < 0 || row >= this._grid.Rows;
                if (row == this._mover.CurrentRow || isNotInBounds || !this._grid.IsPosEmpty(row, this._mover.CurrentCol)) continue;

                if (this._mover.CurrentCol - 1 >= 0)
                {
                    var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol - 1);
                }

                if (this._mover.CurrentCol + 1 < this._grid.Cols)
                {
                    var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol + 1);
                }
            }

            for (int col = this._mover.CurrentCol - 2; col <= this._mover.CurrentCol + 2; col+=2)
            {
                var isNotInBounds = col < 0 || col >= this._grid.Cols;
                if (col == this._mover.CurrentCol || isNotInBounds || !this._grid.IsPosEmpty(this._mover.CurrentRow, col)) continue;

                if (this._mover.CurrentRow - 1 >= 0)
                {
                    var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(this._mover.CurrentRow - 1, col);
                }

                if (this._mover.CurrentRow + 1 < this._grid.Rows)
                {
                    var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(this._mover.CurrentRow + 1, col);
                }
            }
        }
    }
}