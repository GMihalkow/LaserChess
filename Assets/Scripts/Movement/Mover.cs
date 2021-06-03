using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] int _id;

        private GridMap _grid;
        private int _currentCol = -1;
        private int _currentRow = -1;

        public int CurrentCol => this._currentCol;

        public int CurrentRow => this._currentRow;

        private void Awake()
        {
            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public void Move(int row, int col, bool setId = true)
        {
            var prevCol = this._currentCol;
            var prevRow = this._currentRow;

            this.transform.position = this._grid.GetPos(row, col, this._id, setId);

            this._currentCol = col;
            this._currentRow = row;

            if ((prevCol == col && prevRow == row) || (prevRow < 0 || prevCol < 0) || this.CompareTag("PositionMarker")) return;

            this._grid.ClearPos(prevRow, prevCol);
        }
    }
}