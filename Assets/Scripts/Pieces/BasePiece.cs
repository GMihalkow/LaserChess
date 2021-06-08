using LaserChess.Combat;
using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public abstract class BasePiece : MonoBehaviour
    {
        protected Fighter _fighter;
        protected Mover _mover;

        protected virtual void Awake()
        {
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        public void SetupPosition(int row, int col)
        {
            this._mover.Move(row, col);
        }
    }
}