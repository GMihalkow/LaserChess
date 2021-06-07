using LaserChess.Core;
using LaserChess.Pieces.Contracts;
using UnityEngine;

namespace LaserChess.Pieces
{
    public abstract class AIPiece : BasePiece, IAIPiece
    {
        protected GridMap _grid;

        protected override void Awake()
        {
            base.Awake();

            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
        }

        public abstract void Attack();

        public abstract void Move();
    }
}