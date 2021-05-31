using LaserChess.Movement;
using LaserChess.Pieces;
using UnityEngine;

namespace LaserChess.Control
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _playerPieces;

        private void Awake()
        {
            this._playerPieces = GameObject.Find("PlayerPieces");
        }

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, 100f);

            if (hitInfo == default || !Input.GetMouseButtonDown(0)) return;

            if (hitInfo.collider.CompareTag("PlayerPiece"))
            {
                this.HandlePieceInteraction(hitInfo);
            }
            else if (hitInfo.collider.CompareTag("Marker"))
            {
                this.HandlePieceMovement(hitInfo);
            }
        }

        private void HandlePieceInteraction(RaycastHit2D hitInfo)
        {
            var piece = hitInfo.collider.gameObject.GetComponent<Piece>();
            if (piece == null) return;

            if (!piece.IsSelected)
            {
                this.ClearHighlights();
                this.DeselectAllPieces();
                piece.HighlightAvailableSpots();
            }
        }

        private void HandlePieceMovement(RaycastHit2D hitInfo)
        {
            var markerMover = hitInfo.collider.GetComponent<Mover>();
            if (markerMover == null) return;

            var selectedPiece = this.FindSelectedPiece();
            if (selectedPiece == null) return;

            selectedPiece.Move(markerMover.CurrentRow, markerMover.CurrentCol);

            this.ClearHighlights();
            this.DeselectAllPieces();
        }

        private void ClearHighlights()
        {
            foreach (Transform child in this._playerPieces.transform.Find("MarkersContainer"))
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private void DeselectAllPieces()
        {
            foreach (Transform child in this._playerPieces.transform)
            {
                var piece = child.GetComponent<Piece>();
                if (piece == null) continue;

                piece.IsSelected = false;
            }
        }

        private Piece FindSelectedPiece()
        {
            foreach (Transform child in this._playerPieces.transform)
            {
                var piece = child.GetComponent<Piece>();
                if (piece?.IsSelected != true) continue;

                return piece;
            }

            return null;
        }
    }
}