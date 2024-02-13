using UnityEngine;
using UnityEngine.Events;

namespace Submarine
{
    public class HealthModule : BaseModule
    {
        [SerializeField] private int _maxHealthPoints = 200;
        public int HealthPoints { get; private set; }

        [SerializeField] private float _damageTimeout = 0.2f;
        private float _recoveryTime = 0;

        public UnityAction OnDamageReceived;
        public UnityAction OnHealthRestored;
        public UnityAction<int> OnHealthChanged;        

        private void Awake()
        {
            HealthPoints = _maxHealthPoints;
        }

        public void Restore(int points)
        {
            HealthPoints = points;
            OnHealthChanged?.Invoke(points);
            OnHealthRestored?.Invoke();
        }

        public void Damage(int points)
        {
            HealthPoints -= points;
            OnHealthChanged?.Invoke(HealthPoints);
            OnDamageReceived?.Invoke();
            if (HealthPoints <= 0)
                Die();
        }

        /// <summary>
        /// Plays the dead animation and disables the controls
        /// </summary>
        public void Die()
        {
            GameManager.Instance.ShowGameOverScreen();
        }

        public int GetMaxHealth() => _maxHealthPoints;
        public bool HasMaxHealth() => (HealthPoints / GetMaxHealth()) == 1;
    }
}