using UnityEngine;

namespace Submarine
{
    public class ReloadController : SubmarineProcess
    {
        [SerializeField] private AttackModule _attackController;

        public override void StartProcess()
        {
            int currentAmmo = _attackController.GetCannonAmmo();
            base.StartProcess();
        }

        public override void CancelProcess()
        {
            base.CancelProcess();
        }

        protected override void FinishProcess()
        {
            base.FinishProcess();
        }
    }
}