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
            CancelCurrentProcess();
            OnClose?.Invoke();
        }

        public bool AnyProcessRunning() => (_reloadController.IsPerformingProcess || _repairController.IsPerformingProcess);

        public void CancelCurrentProcess()
        {
            if (!AnyProcessRunning())
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

        public void StartReloading()
        {
            if (AnyProcessRunning())
                return;

            if (!CanReload())
                return;

            _reloadController.StartProcess();
        }

        //public bool CanRepair() => !_healthModule.HasMaxHealth() && !_repairController.HasEnemiesAttached();
        //Test
        public bool CanRepair()
        {
            bool canRepair = !_healthModule.HasMaxHealth() && !_healthModule.HasEnemiesAttached();
            if (!canRepair)
                Debug.Log("!_healthModule.HasMaxHealth(): " + !_healthModule.HasMaxHealth() + " && " + "!_healthModule.HasEnemiesAttached(): " + !_healthModule.HasEnemiesAttached());
            return canRepair;
        }

        public bool CanReload() => !_attackModule.HasMaxAmmo();
    }
}