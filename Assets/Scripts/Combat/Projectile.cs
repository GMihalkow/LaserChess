using UnityEngine;

namespace LaserChess.Combat
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _target;

        private void Update()
        {
            if (this._target == default) return;

            this.transform.Translate(Vector2.right * Time.deltaTime);
        }

        public void SetTarget(Vector2 target)
        {
            var screenPos = Camera.main.WorldToScreenPoint(target);
            this.LookAt(screenPos);

            this._target = target;
        }

        /// <summary>
        /// Rotates sprite in the Z to point towards specific point. 
        /// Make sure your sprite is pointing towards the positive X .
        /// </summary>
        /// <param name="screenCoords">in screen point units</param>
        private void LookAt(Vector3 screenCoords)
        {
            var dir = screenCoords - Camera.main.WorldToScreenPoint(this.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}