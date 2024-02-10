using UnityEngine;

namespace Submarine
{
    public class RepairController : SubmarineProcess
    {
        [Tooltip("Health percentage restored per session")]
        [Range(0, 1)]
        [SerializeField] private float _healthPercentage = 0.25f;
        [SerializeField] private HealthModule _healthModule;

        public override void StartProcess()
        {
            float timeMultiplier = (float)_healthModule.HealthPoints / (float)_healthModule.GetMaxHealth();
            Debug.Log(timeMultiplier);
            processTime = fullProcessDuration * timeMultiplier;
            base.StartProcess();
        }

        public override void CancelProcess()
        {
            base.CancelProcess();
            int ammo = (int)(_healthModule.GetMaxHealth() * GetProgress());
            _healthModule.Recover(ammo);
        }

        protected override void FinishProcess()
        {
            base.FinishProcess();            
            _healthModule.Recover(_healthModule.GetMaxHealth());
        }
    }
}