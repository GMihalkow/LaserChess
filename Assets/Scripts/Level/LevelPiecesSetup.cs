using LaserChess.Core.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaserChess.Level
{
    [Serializable]
    public class LevelPiecesSetup
    {
        [SerializeField] DifficultiesEnum _difficulty;
        [SerializeField] List<LevelPieceConfig> _pieces;

        public DifficultiesEnum Difficulty => this._difficulty;

        public List<LevelPieceConfig> Pieces => this._pieces;
    }
}