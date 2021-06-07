using LaserChess.Movement;
using LaserChess.Pieces;
using UnityEngine;

namespace LaserChess.Control
{
    public class PlayerController : MonoBehaviour
    {
        private const float RAYCAST_MAX_DISTANCE = 100f;
        private bool _isDisabled;
        private GameObject _pieces;

        public bool isDisabled 
        {
            get => this._isDisabled;
            set => this._isDisabled = value;
        }

        public int PiecesCount => this._pieces.transform.childCount;

        private void Awake()
        {
            this._isDisabled = true;
            this._pieces = GameObject.Find("PlayerPieces");
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) || this._isDisabled) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, RAYCAST_MAX_DISTANCE);

            if (hitInfo == default) return;

            if (hitInfo.collider.CompareTag("PlayerPiece") && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                this.DisplayMarkers(hitInfo);
            }
            else if (hitInfo.collider.CompareTag("PositionMarker") && Input.GetMouseButtonDown(0))
            {
                this.HandlePieceMovement(hitInfo);
            }
            else if (hitInfo.collider.CompareTag("CombatMarker") && Input.GetMouseButtonDown(0))
            {
                this.HandlePieceCombat(hitInfo.collider.transform.position);
            }
        }

        public void Reset()
        {
            foreach (var piece in this._pieces.GetComponentsInChildren<PlayerPiece>())
            {
                piece.Reset();
            }
        }

        private void HandlePieceCombat(Vector2 markerPos)
        {
            var piece = this.FindSelectedPiece();
            if (piece == null) return;

            piece.Attack(markerPos);
            this.DestroyMarkers();
        }

        private void DisplayMarkers(RaycastHit2D hitInfo)
        {
            var piece = hitInfo.collider.gameObject.GetComponent<PlayerPiece>();
            if (piece == null) return;

            this.DestroyMarkers();
            this.DeselectAllPieces();

            if (Input.GetMouseButtonDown(0))
            {
                piece.HighlightMovementSpots();
            }
            else
            {
                piece.HighlightCombatSpots();
            }
        }

        private void HandlePieceMovement(RaycastHit2D hitInfo)
        {
            var markerMover = hitInfo.collider.GetComponent<Mover>();
            if (markerMover == null) return;

            var selectedPiece = this.FindSelectedPiece();
            if (selectedPiece == null) return;

            selectedPiece.Move(markerMover.CurrentRow, markerMover.CurrentCol);

            this.DestroyMarkers();
            this.DeselectAllPieces();
        }

        private void DestroyMarkers()
        {
            foreach (Transform child in this._pieces.transform.Find("MarkersContainer"))
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private void DeselectAllPieces()
        {
            foreach (Transform child in this._pieces.transform)
            {
                var piece = child.GetComponent<PlayerPiece>();
                if (piece == null) continue;

                piece.IsSelected = false;
            }
        }

        private PlayerPiece FindSelectedPiece()
        {
            foreach (Transform child in this._pieces.transform)
            {
                var piece = child.GetComponent<PlayerPiece>();
                if (piece?.IsSelected != true) continue;

                return piece;
            }

            return null;
        }
    }
}