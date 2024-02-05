using UnityEngine;

namespace Submarine
{
    public class AttackController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private AimController _aimController;
        [SerializeField] private WeaponsController _weaponsController;

        [Space]
        [SerializeField] private float _enemyAttackRadius = 0.3f;

        //TEST ONLY
        [SerializeField] private Transform _enemyAttackPoint;

        private bool _isAttacking = false;

        public void StartAttacking()
        {
            _isAttacking = true;
            if (_weaponsController.CanFireCannon)
                _weaponsController.FireCannon();
        }

        public void StopAttacking()
        {
            _isAttacking = false;
        }

        public void ShootSpecialAttack()
        {
            if (_weaponsController.CanUseAOE)
                _weaponsController.UseAOE();
        }

        public Vector2 GetEnemyAttackPoint()
        {
            int randomAngle = UnityEngine.Random.Range(0, 360);
            float xPosition = _enemyAttackRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float yPosition = _enemyAttackRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

            return new Vector2(xPosition, yPosition);
        }

        void Update()
        {
            if (!_isAttacking)
                return;

            if (_weaponsController.CanFireCannon)
            {
                _weaponsController.SetCannonAimDirection(_aimController.GetAimDirection());
                _weaponsController.FireCannon();
            }

            //TEST ONLY
            if (Input.GetKeyDown(KeyCode.E))
                _enemyAttackPoint.position = GetEnemyAttackPoint() + new Vector2(transform.position.x, transform.position.y);
        }
    }
}