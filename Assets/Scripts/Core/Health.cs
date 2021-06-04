using UnityEngine;

namespace LaserChess.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _hitPoints = 5f;

        private float _currentHitPoints;

        private void Awake()
        {
            this._currentHitPoints = this._hitPoints;
        }

        public void TakeDamage(float dmg)
        {
            this._currentHitPoints = Mathf.Clamp(0, this._currentHitPoints - dmg, this._hitPoints);

            if (!Mathf.Approximately(this._currentHitPoints, 0)) return;

            GameObject.Destroy(this.gameObject);
        }
    }
}