using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            this._health = this.GetComponent<Health>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var projectile = collision.GetComponent<Projectile>();

            if (projectile == null || projectile.CompareTag(this.tag)) return;

            this._health.TakeDamage(projectile.AttackDamage);
            GameObject.Destroy(projectile.gameObject);
        }
    }
}