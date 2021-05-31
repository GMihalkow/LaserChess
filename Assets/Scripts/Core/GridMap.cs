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
        
        public Vector2 GetPos(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols) throw new ArgumentOutOfRangeException("Coordinates are out of grid bounds!");

            var x = (this.xCellSize * (float)row) - (this.xCellSize / 2f);
            var y = (this.yCellSize * (float)col) - (this.yCellSize / 2f);

            return new Vector2(x, y);
        }
    }
}