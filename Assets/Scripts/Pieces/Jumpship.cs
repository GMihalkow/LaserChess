using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Jumpship : PlayerPiece
    {
        public override void HighlightCombatSpots()
        {
            var startCol = Mathf.Max(0, this._mover.CurrentCol - 4);
            var endCol = Mathf.Min(this._grid.Cols - 1, this._mover.CurrentCol + 4);

            var startRow = Mathf.Max(0, this._mover.CurrentRow - 4);
            var endRow = Mathf.Min(this._grid.Rows - 1, this._mover.CurrentRow + 4);

            for (int row = startRow; row <= endRow; row++)
            {
                if (this._mover.CurrentRow == row) continue;

                var marker = GameObject.Instantiate(this._combatMarkerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol, false);
            }

            for (int col = startCol; col <= endCol; col++)
            {
                if (this._mover.CurrentCol == col) continue;

                var marker = GameObject.Instantiate(this._combatMarkerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(this._mover.CurrentRow, col, false);
            }
        }

        public override void HighlightMovementSpots()
        {
            // TODO [GM]: extract code? (in Grunt too)
            if (this._isSelected) return;

            this._isSelected = true;

            for (int row = this._mover.CurrentRow - 2; row <= this._mover.CurrentRow + 2; row+=2)
            {
                var isNotInBounds = row < 0 || row >= this._grid.Rows;
                if (row == this._mover.CurrentRow || isNotInBounds) continue;

                if (this._mover.CurrentCol - 1 >= 0 && this._grid.IsPosEmpty(row, this._mover.CurrentCol - 1))
                {
                    var marker = GameObject.Instantiate(this._movementMarkerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol - 1);
                }

                if (this._mover.CurrentCol + 1 < this._grid.Cols && this._grid.IsPosEmpty(row, this._mover.CurrentCol + 1))
                {
                    var marker = GameObject.Instantiate(this._movementMarkerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol + 1);
                }
            }

            for (int col = this._mover.CurrentCol - 2; col <= this._mover.CurrentCol + 2; col+=2)
            {
                var isNotInBounds = col < 0 || col >= this._grid.Cols;
                if (col == this._mover.CurrentCol || isNotInBounds) continue;

                if (this._mover.CurrentRow - 1 >= 0 && this._grid.IsPosEmpty(this._mover.CurrentRow - 1, col))
                {
                    var marker = GameObject.Instantiate(this._movementMarkerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(this._mover.CurrentRow - 1, col);
                }

                if (this._mover.CurrentRow + 1 < this._grid.Rows && this._grid.IsPosEmpty(this._mover.CurrentRow + 1, col))
                {
                    var marker = GameObject.Instantiate(this._movementMarkerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(this._mover.CurrentRow + 1, col);
                }
            }
        }
    }
}