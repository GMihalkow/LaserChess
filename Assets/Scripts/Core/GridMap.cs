using System;
using UnityEngine;

namespace LaserChess.Core
{
    public class GridMap : MonoBehaviour
    {
        [SerializeField] int rows = 8;
        [SerializeField] int cols = 8;
        [SerializeField] float xCellSize = 0.65f;
        [SerializeField] float yCellSize = 0.7f;
        
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private void Awake()
        {
            this._minX = this.xCellSize / 2f;
            this._maxX = (this.xCellSize * (float)cols) - (this.xCellSize / 2f);

            this._minY = this.yCellSize / 2f;
            this._maxY = (this.yCellSize * (float)rows) - (this.yCellSize / 2f);
        }
        
        public Vector2 GetPos(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols) throw new ArgumentOutOfRangeException("Coordinates are out of grid bounds!");

            var x = (this.xCellSize * (float)col) - (this.xCellSize / 2f);
            var y = (this.yCellSize * (float)row) - (this.yCellSize / 2f);

            return new Vector2(x, y);
        }
    }
}