using LaserChess.Movement;
using LaserChess.Pieces;
using UnityEngine;

namespace LaserChess.Control
{
    public class PlayerController : MonoBehaviour
    {
        private GameObject _pieces;

        private void Awake()
        {
            this._pieces = GameObject.Find("PlayerPieces");
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, 100f);

            if (hitInfo == default) return;

            if (hitInfo.collider.CompareTag("PlayerPiece") && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                this.DisplayMarkers(hitInfo);
            }
            else if (hitInfo.collider.CompareTag("PositionMarker") && Input.GetMouseButtonDown(0))
            {
                this.HandlePieceMovement(hitInfo);
            }
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