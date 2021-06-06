using UnityEngine;

namespace LaserChess.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _attackDamage = 1f;
        [SerializeField] GameObject _projectilePrefab;
        [SerializeField] bool _restrictProjectile = false;
        [SerializeField] float _projectileDestroyOffset = 0.15f;
        [SerializeField] float _destroyTimeout = 2f;

        private GameObject _projectilesContainer;

        private void Awake()
        {
            this._projectilesContainer = GameObject.Find("ProjectilesContainer");
        }

        public void Attack(Vector2 targetPos)
        {
            var projectile = GameObject.Instantiate(this._projectilePrefab, this.transform.position, Quaternion.identity, this._projectilesContainer.transform);
            projectile.GetComponent<Projectile>().SetConfig(targetPos, this._attackDamage, this._projectileDestroyOffset, this._destroyTimeout, this._restrictProjectile);
        }
    }
}