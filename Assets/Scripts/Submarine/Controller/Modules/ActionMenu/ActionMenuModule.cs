using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class ActionMenuModule : BaseModule
    {
        [Header("Repair")]
        [SerializeField] private RepairController _repairController;
        [SerializeField] private HealthModule _healthModule;
        [Header("Reload")]
        [SerializeField] private ReloadController _reloadController;
        [SerializeField] private AttackModule _attackModule;

        public RepairController RepairController => _repairController;
        public ReloadController ReloadController => _reloadController;


        private bool _isOpen = false;
        public bool IsOpen => _isOpen;

        public UnityAction OnOpen;
        public UnityAction OnClose;

        public void Open()
        {
            _isOpen = true;
            OnOpen?.Invoke();
        }

        public void Close()
        {
            _isOpen = false;
            OnClose?.Invoke();
        }

        public bool AnyProcessRunning() => (_reloadController.IsPerformingProcess || _repairController.IsPerformingProcess);

        public void CancelCurrentProcess()
        {
            if (AnyProcessRunning())
                return;

            if (_repairController.IsPerformingProcess)
                _repairController.CancelProcess();

            if (_reloadController.IsPerformingProcess)
                _reloadController.CancelProcess();            
        }

        public void StartRepairing()
        {
            if (AnyProcessRunning())
                return;

            if (!CanRepair())
                return;

            _repairController.StartProcess();
        }

        public void CancelRepairing()
        {
            if (!_repairController.IsPerformingProcess)
                return;

            _repairController.CancelProcess();
        }

        public void StartReloading()
        {
            if (AnyProcessRunning())
                return;

            if (!CanReload())
                return;

            _reloadController.StartProcess();
        }

        public void CancelReloading()
        {
            if (!_reloadController.IsPerformingProcess)
                return;

            _reloadController.CancelProcess();
        }

        public bool CanRepair() => !_healthModule.HasMaxHealth();

        public bool CanReload() => !_attackModule.HasMaxAmmo();
    }
}