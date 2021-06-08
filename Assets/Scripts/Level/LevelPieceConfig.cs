using System;
using UnityEngine;

namespace LaserChess.Level
{
    [Serializable]
    public class LevelPieceConfig
    {
        [SerializeField, Range(0, 7)] int _row;
        [SerializeField, Range(0, 7)] int _col;
        [SerializeField] GameObject _piece;

        public int Row => this._row;

        public int Col => this._col;

        public GameObject Piece => this._piece;
    }
}