﻿using System;
using UnityEngine;

namespace LaserChess.Core
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] int _rows = 8;
        [SerializeField] int _cols = 8;
        [SerializeField] float _xCellSize = 0.65f;
        [SerializeField] float _yCellSize = 0.7f;

        private int[,] _map;

        public int Rows => this._rows;

        public int Cols => this._cols;

        private void Awake()
        {
            this._map = new int[this._rows, this._cols];
        }

        public Vector2 GetPos(int row, int col, int id, bool setId = true)
        {
            this.CheckBounds(row, col);

            var x = (this._xCellSize * (float)row) - (this._xCellSize / 2f);
            var y = (this._yCellSize * (float)col) - (this._yCellSize / 2f);

            if (setId)
            {
                this._map[row, col] = id;
            }

            return new Vector2(x, y);
        }

        public void ClearPos(int row, int col)
        {
            this.CheckBounds(row, col);

            this._map[row, col] = Constants.DEFAULT_CELL_ID;
        }

        public bool IsPosEmpty(int row, int col)
        {
            this.CheckBounds(row, col);

            return this._map[row, col] == Constants.DEFAULT_CELL_ID;
        }

        private void CheckBounds(int row, int col)
        {
            if (row < 0 || row >= this._rows || col < 0 || col >= this._cols) throw new ArgumentOutOfRangeException("Coordinates are out of grid bounds!");
        }
    }
}