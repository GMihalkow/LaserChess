using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _attackDamage = 1f;
        [SerializeField] GameObject _projectilePrefab;

        public void Attack(CombatTarget target)
        {
            var projectile = GameObject.Instantiate(this._projectilePrefab, this.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetTarget(target.transform.position);

            var targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(this._attackDamage);
        }
    }
}