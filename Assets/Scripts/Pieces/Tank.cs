using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Tank : PlayerPiece
    {
        public override void HighlightAvailableSpots()
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

                    var marker = GameObject.Instantiate(this._markerPrefab, this._markersContainer.transform);
                    marker.GetComponent<Mover>().Move(row, col);
                }
            }
        }
    }
}