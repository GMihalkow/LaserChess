using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Tank : PlayerPiece
    {
        public override void HighlightCombatSpots()
        {
            this._isSelected = true;

            for (int row = 0; row < this._grid.Rows; row++)
            {
                if (this._mover.CurrentRow == row) continue;

                var marker = GameObject.Instantiate(this._combatMarkerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(row, this._mover.CurrentCol, false);
            }

            for (int col = 0; col < this._grid.Cols; col++)
            {
                if (this._mover.CurrentCol == col) continue;

                var marker = GameObject.Instantiate(this._combatMarkerPrefab, this._markersContainer.transform);
                marker.GetComponent<Mover>().Move(this._mover.CurrentRow, col, false);
            }
        }

        public override void HighlightMovementSpots()
        {
            if (this._isSelected) return;

            this._isSelected = true;

            var startRow = Mathf.Max(this._mover.CurrentRow - 3, 0);
            var startCol = Mathf.Max(this._mover.CurrentCol - 3, 0);

            var endRow = Mathf.Min(this._mover.CurrentRow + 3, this._grid.Rows - 1);
            var endCol = Mathf.Min(this._mover.CurrentCol + 3, this._grid.Cols - 1);

            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = startCol; col <= endCol; col++)
                {
                    var isPosEmpty = this._grid.IsPosEmpty(row, col);

                    if ((col == this._mover.CurrentCol && row == this._mover.CurrentRow) || !isPosEmpty) continue;

                    var marker = GameObject.Instantiate(this._movementMarkerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, col);
                }
            }
        }
    }
}