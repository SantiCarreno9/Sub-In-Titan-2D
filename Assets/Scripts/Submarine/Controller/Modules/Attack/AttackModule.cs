using UnityEngine;

namespace Submarine
{
    public class AttackModule : BaseModule
    {
        [Header("Components")]
        [SerializeField] private AimController _aimController;
        [SerializeField] private WeaponsController _weaponsController;

        [Space]
        [SerializeField] private float _enemyAttackRadius = 0.3f;

        //TEST ONLY
        [SerializeField] private Transform _enemyAttackPoint;

        private bool _isFiringBasicAttack = false;

        public override void EnableModule()
        {
            base.EnableModule();
            _aimController.enabled = true;
        }

        public override void DisableModule()
        {
            base.DisableModule();
            _aimController.enabled = false;
        }

        #region BASIC ATTACK

        public void StartBasicAttack()
        {
            if (!IsEnabled)
                return;
            _isFiringBasicAttack = true;
            //TEST
            if (_weaponsController.CanFireCannon)
            {
                _weaponsController.SetCannonAimDirection(_aimController.GetAimDirection());
                _weaponsController.FireCannon();
            }
        }

        public void StopBasicAttack()
        {
            if (!IsEnabled)
                return;
            _isFiringBasicAttack = false;
            Debug.Log("Stop Basic Attack");
        }

        public void ReloadCannon(int ammo)
        {
            _weaponsController.ReloadCannon(ammo);
        }

        public int GetCannonAmmo() => _weaponsController.CurrentCannonAmmo;

        #endregion

        #region SPECIAL ATTACK (AOE)

        public void StartSpecialAttack()
        {
            if (!IsEnabled)
                return;

            if (_weaponsController.CanUseAOE)
                _weaponsController.ChargeAOE();
        }

        public void StopSpecialAttack()
        {
            if (!IsEnabled)
                return;

            if (_weaponsController.CanUseAOE)
            {
                if (_weaponsController.IsAOEReady)
                    _weaponsController.UseAOE();
                else _weaponsController.CancelAOE();
            }
        }

        #endregion

        /// <summary>
        /// Called by the enemy to select a position to attack
        /// </summary>
        /// <returns></returns>
        public Vector2 GetEnemyAttackPoint()
        {
            int randomAngle = UnityEngine.Random.Range(0, 360);
            float xPosition = _enemyAttackRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float yPosition = _enemyAttackRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

            return new Vector2(xPosition, yPosition);
        }

        void Update()
        {
            if (!IsEnabled)
                return;
            //if (_isFiringBasicAttack)
            //{
            //    if (_weaponsController.CanFireCannon)
            //    {
            //        _weaponsController.SetCannonAimDirection(_aimController.GetAimDirection());
            //        _weaponsController.FireCannon();
            //    }
            //}

            //TEST ONLY
            //if (Input.GetKeyDown(KeyCode.E))
            //    _enemyAttackPoint.position = GetEnemyAttackPoint() + new Vector2(transform.position.x, transform.position.y);
        }
    }
}