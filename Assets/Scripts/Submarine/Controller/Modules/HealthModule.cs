using UnityEngine;

namespace Submarine
{
    public class HealthModule : BaseModule, IHealth
    {
        [SerializeField] private int _maxHealthPoints = 200;
        public int HealthPoints => _healthPoints;
        private int _healthPoints = 0;

        [SerializeField] private float _damageTimeout = 0.2f;
        private float _recoveryTime = 0;

        public void Damage(int points)
        {
            _healthPoints -= points;
            if (_healthPoints <= 0)
                Die();
            else Damage();
        }

        /// <summary>
        /// Triggers the "getting hit" animation and updates the recovery time
        /// that has to pass to allow the user to control the character
        /// </summary>
        public void Damage()
        {
            _recoveryTime = Time.time + _damageTimeout;
        }

        /// <summary>
        /// Plays the dead animation and disables the controls
        /// </summary>
        public void Die()
        {
            //_animator.Play("Die");
            GameManager.Instance.ShowGameOverScreen();
        }
    }
}