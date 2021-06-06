using LaserChess.Core;
using LaserChess.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class CommandUnit : BasePiece
    {
        private GridMap _grid;

        protected override void Awake()
        {
            base.Awake();

            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public void Move()
        {
            var allPieces = this._grid.GetAllPiecesPositions();

            var isInDanger = this.IsSpotInDanger(allPieces, this._mover.CurrentRow, this._mover.CurrentCol);
            if (!isInDanger) return;

            var closestClearSpot = this.FindClosestClearSpot(allPieces);
            if (closestClearSpot == null) return;

            var nextRow = closestClearSpot[0];
            var nextCol = closestClearSpot[1];

            this._mover.Move(nextRow, nextCol);
        }

        private bool IsSpotInDanger(int[][] allPieces, int currentRow, int currentCol)
        {
            foreach (var coords in allPieces)
            {
                var row = coords[0];
                var col = coords[1];

                if (!this._grid.HasPlayerPieceOnPos(row, col)) continue;

                var id = this._grid.GetIdOnPos(row, col);
                var enemyOnSameDiagonal = Mathf.Abs(row - col) == Mathf.Abs(currentRow - currentCol) || row + col == currentRow + currentCol;

                if (enemyOnSameDiagonal)
                {
                    if (id == (int)PiecesIdsEnum.Grunt)
                    {
                        if (Mathf.Abs(row - col) == Mathf.Abs(currentRow - currentCol))
                        {
                            var piecesOnDiagonal = this.GetPiecesOnDiagonal(allPieces, (innerRow, innerCol) => Mathf.Abs(innerRow - innerCol) == Mathf.Abs(currentRow - currentCol));
                            var somethingIsBetween = this.CheckDiagonalForPiecesBetween(piecesOnDiagonal, row, col);

                            if (somethingIsBetween) continue;
                            return true;
                        }
                        else
                        {
                            var piecesOnDiagonal = this.GetPiecesOnDiagonal(allPieces, (innerRow, innerCol) => innerRow + innerCol == currentRow + currentCol);
                            var somethingIsBetween = this.CheckDiagonalForPiecesBetween(piecesOnDiagonal, row, col);

                            if (somethingIsBetween) continue;
                            return true;
                        }
                    }
                }

                var enemyOnSameOrthogonal = row == currentRow || col == currentCol;

                if (!enemyOnSameOrthogonal) continue;

                if (id == (int)PiecesIdsEnum.Tank || id == (int)PiecesIdsEnum.Jumpship && Mathf.Abs(row - currentRow) <= 4)
                {
                    if (row == currentRow)
                    {
                        var piecesOnVertical = allPieces.Where((coord) => coord[0] == row).ToArray();
                        var pieceExistsBetween = this.CheckVerticallyForPiecesBetween(piecesOnVertical, row, col);
                        if (pieceExistsBetween) continue;

                        return true;
                    }
                    else
                    {
                        var piecesOnHorizontal = allPieces.Where((coord) => coord[1] == col).ToArray();
                        var pieceExistsBetween = this.CheckHorizontallyForPiecesBetween(piecesOnHorizontal, row, col);
                        if (pieceExistsBetween) continue;

                        return true;
                    }
                }
            }

            return false;
        }

        private int[] FindClosestClearSpot(int[][] allPieces)
        {
            var clearSpots = new List<int[]>();

            for (int col = 0; col < this._grid.Cols; col++)
            {
                if (this._mover.CurrentCol == col || this.IsSpotInDanger(allPieces, this._mover.CurrentRow, col)) continue;

                clearSpots.Add(new int[] { this._mover.CurrentRow, col });
            }

            return clearSpots.OrderBy((c) => Mathf.Abs(c[1] - this._mover.CurrentCol)).FirstOrDefault();
        }

        private bool CheckDiagonalForPiecesBetween(List<int[]> piecesOnDiagonal, int pieceRow, int pieceCol)
        {
            foreach (var coords in piecesOnDiagonal)
            {
                var row = coords[0];
                var col = coords[1];

                if ((row == this._mover.CurrentRow && col == this._mover.CurrentCol) || (row == pieceRow && col == pieceCol)) continue;

                var isBetween = (row < pieceRow && col > pieceCol && this._mover.CurrentCol > col && this._mover.CurrentRow < row) ||
                    (row > pieceRow && col < pieceCol && this._mover.CurrentCol < col && this._mover.CurrentRow > row) ||
                    (row > pieceRow && col > pieceCol && this._mover.CurrentCol > col && this._mover.CurrentRow > row) ||
                    (row < pieceRow && col < pieceCol && this._mover.CurrentCol < col && this._mover.CurrentRow < row);

                if (!isBetween) continue;

                return true;
            }

            return false;
        }

        private bool CheckVerticallyForPiecesBetween(int[][] piecesOnVertical, int row, int col)
        {
            if (col > this._mover.CurrentCol)
            {
                return piecesOnVertical.Any((coords) =>
                {
                    if (col == coords[1] && row == coords[0]) return false;

                    return coords[1] > this._mover.CurrentCol && coords[1] < col;
                });
            }
            else
            {
                return piecesOnVertical.Any((coords) =>
                {
                    if (col == coords[1] && row == coords[0]) return false;

                    return coords[1] < this._mover.CurrentCol && coords[1] > col;
                });
            }
        }

        private bool CheckHorizontallyForPiecesBetween(int[][] piecesOnHorizontal, int row, int col)
        {
            if (row > this._mover.CurrentRow)
            {
                return piecesOnHorizontal.Any((coords) =>
                {
                    if (col == coords[1] && row == coords[0]) return false;

                    return coords[0] > this._mover.CurrentRow && coords[0] < row;
                });
            }
            else
            {
                return piecesOnHorizontal.Any((coords) =>
                {
                    if (col == coords[1] && row == coords[0]) return false;

                    return coords[0] < this._mover.CurrentRow && coords[0] > row;
                });
            }
        }

        private List<int[]> GetPiecesOnDiagonal(int[][] pieces, Func<int, int, bool> checkDiagonalFunc)
        {
            var piecesOnDiagonal = new List<int[]>();

            foreach (var coords in pieces)
            {
                var row = coords[0];
                var col = coords[1];

                var isOnSameDiagonal = checkDiagonalFunc(row, col);

                if (!isOnSameDiagonal || (row == this._mover.CurrentRow && col == this._mover.CurrentCol)) continue;

                piecesOnDiagonal.Add(new int[] { row, col });
            }

            return piecesOnDiagonal;
        }
    }
}