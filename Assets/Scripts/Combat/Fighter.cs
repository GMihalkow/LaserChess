using UnityEngine;

namespace LaserChess.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _attackDamage = 1f;
        [SerializeField] GameObject _projectilePrefab;
        [SerializeField] bool _restrictProjectile = false;
        [SerializeField] float _projectileDestroyOffset = 0.15f;

        public void Attack(Vector2 targetPos)
        {
            var projectile = GameObject.Instantiate(this._projectilePrefab, this.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetConfig(targetPos, this._attackDamage, this._projectileDestroyOffset, this._restrictProjectile);
        }
    }
}