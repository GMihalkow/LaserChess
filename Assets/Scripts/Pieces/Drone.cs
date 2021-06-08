using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaserChess.Pieces
{
    public class Drone : AIPiece
    {
        private bool _hasReachedLastRow;

        public bool HasReachedLastRow => this._hasReachedLastRow;

        public override void Attack()
        {
            var enemy = this.FindEnemy();
            if (enemy == null) return;

            var enemyPos = this._grid.GetPosWorldCoords(enemy[0], enemy[1], setId: false);
            this._fighter.Attack(enemyPos);
        }

        public override void Move()
        {
            var row = Mathf.Max(this._mover.CurrentRow - 1, 0);
            if (!this._grid.IsPosEmpty(row, this._mover.CurrentCol)) return;

            this._mover.Move(row, this._mover.CurrentCol);
            this._hasReachedLastRow = row == 0;
        }

        private int[] FindEnemy()
        {
            var pieces = this._grid.GetAllPiecesPositions();
            var firstDiagonalPieces = this.GetPiecesOnDiagonal(pieces, (row, col) => Mathf.Abs(row - col) == Mathf.Abs(this._mover.CurrentRow - this._mover.CurrentCol));
            var validPieceOnFirstDiagonal = this.GetValidPieceFromDiagonal(firstDiagonalPieces);

            if (validPieceOnFirstDiagonal != null) return validPieceOnFirstDiagonal;

            var secondDiagonalPieces = this.GetPiecesOnDiagonal(pieces, (row, col) => row + col == this._mover.CurrentCol + this._mover.CurrentRow);
            var validPieceOnSecondDiagonal = this.GetValidPieceFromDiagonal(secondDiagonalPieces);

            if (validPieceOnSecondDiagonal != null) return validPieceOnSecondDiagonal;

            return null;
        }

        private int[] GetValidPieceFromDiagonal(List<int[]> piecesOnDiagonal)
        {
            foreach (var coords in piecesOnDiagonal)
            {
                var row = coords[0];
                var col = coords[1];

                if (!this._grid.HasPlayerPieceOnPos(row, col)) continue;

                var pieceExistsBetween = piecesOnDiagonal.Any((coord) =>
                {
                    var isPlayerPiece = this._grid.HasPlayerPieceOnPos(coord[0], coord[1]);

                    return (coord[0] < row && coord[1] > col && this._mover.CurrentCol > coord[1] && this._mover.CurrentRow < coord[0] && !isPlayerPiece) ||
                        (coord[0] > row && coord[1] < col && this._mover.CurrentCol < coord[1] && this._mover.CurrentRow > coord[0] && !isPlayerPiece) ||
                        (coord[0] > row && coord[1] > col && this._mover.CurrentCol > coord[1] && this._mover.CurrentRow > coord[0] && !isPlayerPiece) ||
                        (coord[0] < row && coord[1] < col && this._mover.CurrentCol < coord[1] && this._mover.CurrentRow < coord[0] && !isPlayerPiece);
                });

                if (pieceExistsBetween) continue;

                return coords;
            }

            return null;
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