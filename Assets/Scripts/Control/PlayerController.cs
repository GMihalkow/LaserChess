using LaserChess.Movement;
using UnityEngine;

namespace LaserChess.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(ray.origin, ray.direction, 100f);

            if (hitInfo == default || !hitInfo.collider.CompareTag("PlayerPiece")) return;

            if (Input.GetMouseButtonDown(0))
            {
                // TODO [GM]: implement movement logic
            }
        }
    }
}