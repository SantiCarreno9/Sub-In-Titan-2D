using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class ActionMenuModule : BaseModule
    {
        [SerializeField] private RepairController _repairController;
        [SerializeField] private ReloadController _reloadController;


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
        public bool IsRepairing() => _repairController.IsPerformingProcess;
        public bool IsReloading() => _reloadController.IsPerformingProcess;

        public float GetRepairingProcess() => _repairController.GetProgress();
        public float GetReloadingProcess() => _reloadController.GetProgress();

        public void StartRepairing()
        {
            if (AnyProcessRunning())
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

            _reloadController.StartProcess();
        }

        public void CancelReloading()
        {
            if (!_reloadController.IsPerformingProcess)
                return;

            _reloadController.CancelProcess();
        }

    }
}