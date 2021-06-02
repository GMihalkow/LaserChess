using LaserChess.Core;
using LaserChess.Pieces.Contracts;
using UnityEngine;

namespace LaserChess.Pieces
{
    public abstract class PlayerPiece : BasePiece, IPlayerPiece
    {
        [SerializeField] protected GameObject _markerPrefab;

        protected GridMap _grid;
        protected GameObject _markersContainer;
        protected bool _isSelected;

        public bool IsSelected
        {
            get => this._isSelected;
            set => this._isSelected = value;
        }

        protected override void Awake()
        {
            base.Awake();

            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
            this._markersContainer = this.transform.parent.Find("MarkersContainer").gameObject;
        }

        public abstract void HighlightAvailableSpots();

        public virtual void Move(int row, int col)
        {
            this._mover.Move(row, col);
        }
    }
}