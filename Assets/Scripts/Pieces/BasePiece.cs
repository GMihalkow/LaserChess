﻿using LaserChess.Combat;
using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Pieces
{
    public abstract class BasePiece : MonoBehaviour
    {
        [SerializeField] int _initialCol;
        [SerializeField] int _initialRow;
        
        protected Fighter _fighter;
        protected Mover _mover;

        protected virtual void Awake()
        {
            this._fighter = this.GetComponent<Fighter>();
            this._mover = this.GetComponent<Mover>();
        }

        private void Start()
        {
            this._mover.Move(this._initialRow, this._initialCol);
        }
    }
}