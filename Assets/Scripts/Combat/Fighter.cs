using UnityEngine;

namespace LaserChess.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _attackDamage = 1f;
        [SerializeField] GameObject _projectilePrefab;

        public void Attack(Vector2 targetPos)
        {
            var projectile = GameObject.Instantiate(this._projectilePrefab, this.transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetTarget(targetPos);
        }
    }
}