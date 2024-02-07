using UnityEngine;

namespace Submarine
{
    public class RepairController : SubmarineProcess
    {
        [Tooltip("Health percentage restored per session")]
        [Range(0, 1)]
        [SerializeField] private float _healthPercentage = 0.25f;

        public override void StartProcess()
        {
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