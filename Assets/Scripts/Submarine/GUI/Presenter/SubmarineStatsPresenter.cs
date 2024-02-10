using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarine.UI
{
    public class SubmarineStatsPresenter : MonoBehaviour
    {
        [SerializeField] private SubmarineStatsView _view;
        [SerializeField] private HealthModule _healthModule;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private MovementModule _movementModule;

        private void OnEnable()
        {
            _healthModule.OnHealthChanged += UpdateHealth;
            _attackModule.OnBasicAttackShot += UpdateAmmo;
            _attackModule.OnCannonReloaded += UpdateAmmo;
        }

        private void OnDisable()
        {
            _healthModule.OnHealthChanged -= UpdateHealth;
            _attackModule.OnBasicAttackShot -= UpdateAmmo;
            _attackModule.OnCannonReloaded -= UpdateAmmo;
        }

        private void Start()
        {
            UpdateAmmo();
            UpdateHealth(_healthModule.HealthPoints);
        }

        private void UpdateAmmo()
        {
            _view.UpdateAmmo(_attackModule.GetCannonAmmo());
        }

        private void UpdateHealth(int points)
        {
            float percentage = (float)points / (float)_healthModule.GetMaxHealth();
            _view.UpdateHealth(percentage);
        }

        private void Update()
        {
            _view.UpdateDash(_movementModule.GetRemainingDashPercentage());
        }

    }
}