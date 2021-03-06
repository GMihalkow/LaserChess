using UnityEngine;
using UnityEngine.Events;

namespace LaserChess.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] UnityEvent _onDeath;
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

            this._onDeath.Invoke();
            GameObject.Destroy(this.gameObject);
        }
    }
}