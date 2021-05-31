using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Movement
{
    public class Mover : MonoBehaviour
    {
        private GridMap _grid;
        private int _currentCol;
        private int _currentRow;

        public int CurrentCol => this._currentCol;

        public int CurrentRow => this._currentRow;

        private void Awake()
        {
            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public void Move(int row, int col)
        {
            this.transform.position = this._grid.GetPos(row, col);
            this._currentCol = col;
            this._currentRow = row;
        }
    }
}