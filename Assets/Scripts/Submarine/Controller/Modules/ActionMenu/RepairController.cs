using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class RepairController : SubmarineProcess
    {
        [Tooltip("Health percentage restored per session")]        
        [SerializeField] private HealthModule _healthModule;
        private short _enemiesAttachedCount = 0;

        public UnityAction OnEnemyAttached;
        public UnityAction OnEnemyRemoved;

        public override void StartProcess()
        {
            float timeMultiplier = (float)_healthModule.HealthPoints / (float)_healthModule.GetMaxHealth();            
            processTime = fullProcessDuration * timeMultiplier;
            base.StartProcess();
        }

        public override void CancelProcess()
        {
            base.CancelProcess();
            int ammo = (int)(_healthModule.GetMaxHealth() * GetProgress());
            _healthModule.Restore(ammo);
        }

        protected override void FinishProcess()
        {
            _healthModule.Restore(_healthModule.GetMaxHealth());
            base.FinishProcess();
        }

        protected override void Update()
        {
            base.Update();
            if (IsPerformingProcess)
            {
                int health = (int)(_healthModule.GetMaxHealth() * GetProgress());                
                _healthModule.Restore(health);
            }
        }

        public void AddAttachedEnemy()
        {
            _enemiesAttachedCount++;
        }

        public void RemoveAttachedEnemy()
        {
            _enemiesAttachedCount--;
            if (_enemiesAttachedCount < 0)
                _enemiesAttachedCount = 0;
        }

        public bool HasEnemiesAttached() => _enemiesAttachedCount > 0;
    }
}