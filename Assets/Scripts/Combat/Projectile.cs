using UnityEngine;

namespace LaserChess.Combat
{
    public class Projectile : MonoBehaviour
    {
        private Vector2 _target;

        private void Update()
        {
            if (this._target == default) return;

            this.transform.position = Vector2.MoveTowards(this.transform.position, this._target, 1f);
        }

        public void SetTarget(Vector2 target)
        {
            this._target = target;
        }
    }
}