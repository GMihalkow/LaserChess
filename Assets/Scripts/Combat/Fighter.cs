using LaserChess.Core;
using UnityEngine;

namespace LaserChess.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _attackDamage = 1f;

        public void Attack(CombatTarget target)
        {
            // TODO [GM]: shoot laser beem

            var targetHealth = target.GetComponent<Health>();
            targetHealth.TakeDamage(this._attackDamage);
        }
    }
}