using UnityEngine;

namespace LaserChess.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _speed = 5f;

        private float _attackDamage;
        private Vector2 _target;
        private bool _destroyAtPos;
        private float _destroyOffset;

        public float AttackDamage => this._attackDamage;

        private void Update()
        {
            if (this._target == default) return;

            this.transform.Translate((Vector2.right * this._speed) * Time.deltaTime);
        }

        private void LateUpdate()
        {
            Vector2 screenPos = Camera.main.WorldToViewportPoint(this.transform.position);
            var isVisible = (screenPos.x >= 0 && screenPos.y >= 0) && (screenPos.x <= 1 && screenPos.y <= 1);
            
            if (!isVisible)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            if (!this._destroyAtPos) return;

            var vector2Pos = new Vector2(this.transform.position.x, this.transform.position.y);
            var distance = Vector2.Distance(this._target, vector2Pos);

            if (this._target == default || distance > this._destroyOffset) return;

            GameObject.Destroy(this.gameObject);
        }

        public void SetConfig(Vector2 target, float attackDamage, float destroyOffset, float destroyTimeout, bool destroyAtPos = true)
        {
            this._destroyOffset = destroyOffset;
            this._destroyAtPos = destroyAtPos;
            this._attackDamage = attackDamage;

            GameObject.Destroy(this.gameObject, destroyTimeout);

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