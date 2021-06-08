using LaserChess.Control;
using LaserChess.Core.Enums;
using LaserChess.Movement;
using LaserChess.Pieces;
using LaserChess.Pieces.Contracts;
using System.Collections.Generic;
using UnityEngine;

namespace LaserChess.AI
{
    public class AIController : BaseController
    {
        [SerializeField] float _delayBetweenTurns = 1f;

        public float DelayBetweenPieceTurns => this._delayBetweenTurns;

        public int PiecesCount => this._pieces.transform.childCount;

        public bool HasCommandUnit => this._pieces.GetComponentInChildren<CommandUnit>() != null;

        private void Awake()
        {
            this._pieces = GameObject.Find("AIPieces");
        }
        
        public void MovePieces(PiecesIdsEnum id)
        {
            if (id != PiecesIdsEnum.CommandUnit && id != PiecesIdsEnum.Dreadnought && id != PiecesIdsEnum.Drone) return;

            var filteredPieces = this.GetPiecesById(id);

            foreach (var piece in filteredPieces)
            {
                if (piece is IAIPiece)
                {
                    ((IAIPiece)piece).Move();
                }
                else if (id == PiecesIdsEnum.CommandUnit)
                {
                    ((CommandUnit)piece).Move();
                }
            }
        }

        public void Attack(PiecesIdsEnum id)
        {
            if (id != PiecesIdsEnum.Dreadnought && id != PiecesIdsEnum.Drone) return;
            
            var filteredPieces = this.GetPiecesById(id);

            foreach (var piece in filteredPieces)
            {
                if (!(piece is IAIPiece)) continue;

                ((IAIPiece)piece).Attack();
            }
        }

        private BasePiece[] GetPiecesById(PiecesIdsEnum id)
        {
            var allPieces = this._pieces.GetComponentsInChildren<BasePiece>();
            var filteredPieces = new List<BasePiece>();

            foreach (var piece in allPieces)
            {
                if (piece.GetComponent<Mover>().Id != id) continue;

                filteredPieces.Add(piece);
            }

            return filteredPieces.ToArray();
        }
    }
}