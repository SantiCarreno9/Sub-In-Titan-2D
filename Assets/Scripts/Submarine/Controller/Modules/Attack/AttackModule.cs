using Submarine.Test;
using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class AttackModule : BaseModule
    {
        [Header("Components")]
        [SerializeField] private AimController _aimController;
        //[SerializeField] private WeaponsController _weaponsController;
        [SerializeField] private SubWeaponsHandler _weaponsController;

        public AimController AimController => _aimController;

        [Space]
        [SerializeField] private float _enemyAttackRadius = 0.3f;

        //TEST ONLY
        [SerializeField] private Transform _enemyAttackPoint;

        public UnityAction OnBasicAttackShot;
        public UnityAction OnCannonReloaded;

        public UnityAction OnAOECharge;
        public UnityAction OnAOEShot;
        public UnityAction OnAOECanceled;

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
                OnBasicAttackShot?.Invoke();
            }
        }

        public void StopBasicAttack()
        {
            if (!IsEnabled)
                return;
            _isFiringBasicAttack = false;
        }

        public void ReloadCannon(int ammo)
        {
            _weaponsController.ReloadCannon(ammo);
            OnCannonReloaded?.Invoke();
        }

        public int GetCannonAmmo() => _weaponsController.CurrentCannonAmmo;
        public int GetMaxCannonAmmo() => _weaponsController.MaxAmmo;
        public bool HasMaxAmmo() => ((float)GetCannonAmmo() / (float)GetMaxCannonAmmo()) == 1;

        #endregion

        #region SPECIAL ATTACK (AOE)

        public void StartSpecialAttack()
        {
            if (!IsEnabled)
                return;

            if (_weaponsController.CanUseAOE)
            {
                _weaponsController.ChargeAOE();
                OnAOECharge?.Invoke();
            }
        }

        public void StopSpecialAttack()
        {
            if (!IsEnabled)
                return;

            if (_weaponsController.CanUseAOE)
            {
                if (_weaponsController.IsAOEReady)
                {
                    _weaponsController.UseAOE();
                    OnAOEShot?.Invoke();
                }
                else
                {
                    _weaponsController.CancelAOE();
                    OnAOECanceled?.Invoke();
                }
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