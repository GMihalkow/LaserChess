using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Dreadnought : AIPiece
    {
        private GridMap _grid;

        protected override void Awake()
        {
            base.Awake();

            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public override void Move()
        {
            var closestEnemyPos = this.FindClosestEnemy();

            if (closestEnemyPos == null || closestEnemyPos.Length < 2) return;

            var row = closestEnemyPos[0];
            var col = closestEnemyPos[1];

            var rowIncrement = this._mover.CurrentRow > row ? -1 : this._mover.CurrentRow == row ? 0 : 1;
            var colIncrement = this._mover.CurrentCol > col ? -1 : this._mover.CurrentCol == col ? 0 : 1;

            var nextRow = this._mover.CurrentRow + rowIncrement;
            var nextCol = this._mover.CurrentCol + colIncrement;

            this.ExecuteMovement(row, col, nextRow, nextCol);
        }

        private void ExecuteMovement(int currentRow, int currentCol, int nextRow, int nextCol)
        {
            if (nextCol != currentCol && nextRow != currentRow)
            {
                var firstAttempt = this._grid.IsPosEmpty(nextRow, nextCol);
                var secondAttempt = this._grid.IsPosEmpty(nextRow, currentCol);
                var thirdAttempt = this._grid.IsPosEmpty(currentRow, nextCol);

                if (firstAttempt)
                {
                    this._mover.Move(nextRow, nextCol);
                }
                else if (secondAttempt)
                {
                    this._mover.Move(nextRow, currentCol);
                }
                else if (thirdAttempt)
                {
                    this._mover.Move(currentRow, nextCol);
                }
            }
            else
            {
                var rowIncrement = 0;
                var colIncrement = 0;

                if (nextCol == currentCol)
                {
                    colIncrement = 1;
                }
                else
                {
                    rowIncrement = 1;
                }

                var firstAttempt = this._grid.IsPosEmpty(nextRow, nextCol);
                var secondAttempt = this._grid.IsPosEmpty(nextRow + rowIncrement, currentCol + colIncrement);
                var thirdAttempt = this._grid.IsPosEmpty(currentRow - rowIncrement, nextCol - colIncrement);

                if (firstAttempt)
                {
                    this._mover.Move(nextRow, nextCol);
                }
                else if (secondAttempt)
                {
                    this._mover.Move(Mathf.Min(this._grid.Rows - 1, nextRow + rowIncrement), Mathf.Min(this._grid.Rows - 1, currentCol + colIncrement));
                }
                else if (thirdAttempt)
                {
                    this._mover.Move(Mathf.Max(0, currentRow - rowIncrement), Mathf.Max(0, nextCol - colIncrement));
                }
            }
        }

        private int[] FindClosestEnemy()
        {
            for (int index = 1; index < this._grid.Rows; index++)
            {
                var startRow = Mathf.Max(this._mover.CurrentRow - index, 0);
                var startCol = Mathf.Max(this._mover.CurrentCol - index, 0);

                var endRow = Mathf.Min(this._mover.CurrentRow + index, this._grid.Rows - 1);
                var endCol = Mathf.Min(this._mover.CurrentCol + index, this._grid.Cols - 1);

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = startCol; col <= endCol; col++)
                    {
                        if (col == this._mover.CurrentCol && row == this._mover.CurrentRow) continue;

                        var playerIsOnPos = this._grid.HasPlayerPieceOnPos(row, col);

                        if (!playerIsOnPos) continue;

                        return new int[] { row, col };
                    }
                }
            }

            return null;
        }
    }
}