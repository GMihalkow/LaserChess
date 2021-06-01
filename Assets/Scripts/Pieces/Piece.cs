using LaserChess.Core;
using LaserChess.Movement;
using LaserChess.Pieces.Contracts;
using UnityEngine;

namespace LaserChess.Pieces
{
    public abstract class Piece : MonoBehaviour, IPiece
    {
        [SerializeField] protected int _initialCol;
        [SerializeField] protected int _initialRow;
        [SerializeField] protected GameObject _markerPrefab;

        protected GridMap _grid;
        protected GameObject _markersContainer;
        protected Mover _mover;
        protected bool _isSelected;

        public bool IsSelected
        {
            get => this._isSelected;
            set => this._isSelected = value;
        }

        protected virtual void Awake()
        {
            this._grid = GameObject.Find("GridMap").GetComponent<GridMap>();
            this._markersContainer = this.transform.parent.Find("MarkersContainer").gameObject;
            this._mover = this.GetComponent<Mover>();
        }

        private void Start()
        {
            this._mover.Move(this._initialRow, this._initialCol);
        }

        public abstract void HighlightAvailableSpots();

        public abstract void Move(int row, int col);
    }
}