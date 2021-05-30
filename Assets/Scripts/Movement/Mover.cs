using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Movement
{
    public class Mover : MonoBehaviour
    {
        private GridMap _grid;

        private void Awake()
        {
            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public void Move(int row, int col)
        {
            this.transform.position = this._grid.GetPos(row, col);
        }
    }
}