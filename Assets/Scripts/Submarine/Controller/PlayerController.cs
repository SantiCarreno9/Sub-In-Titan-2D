using UnityEngine;
using UnityEngine.InputSystem;

namespace Submarine
{
    public class PlayerController : MonoBehaviour, IHealth, ISubmarine
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private WeaponsController _weaponsController;
        private PlayerInputs _inputs;


        [SerializeField] private float _damageTimeout = 0.2f;
        private float _recoveryTime = 0;

        [Header("Animations")]
        [SerializeField] private Animator _animator;

        [Header("Health")]
        [SerializeField] private int _maxHealthPoints = 200;
        public int HealthPoints => _healthPoints;
        private int _healthPoints = 0;

        private void Awake()
        {
            _inputs = new PlayerInputs();
            _inputs.Player.Move.performed += Move_performed;
            _inputs.Player.Move.canceled += Move_canceled;
            _inputs.Player.Dash.performed += Dash_performed;
            _inputs.Player.Dash.canceled += Dash_canceled;

            _inputs.Player.Fire.performed += Fire_performed;
            _inputs.Player.Fire.canceled += Fire_canceled;
            _inputs.Player.SpecialAttack.performed += SpecialAttack_performed;
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }

        #region USER INPUTS EVENTS

        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _movementController.SetUserMovementInput(obj.ReadValue<Vector2>());
        }

        /// <summary>
        /// Resets the movement Vector and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _movementController.SetUserMovementInput(Vector2.zero);
        }

        private void Dash_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _movementController.StartDashing();
        }

        private void Dash_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _movementController.StopDashing();
        }

        /// <summary>
        /// Ensures that the user is able to attack, if so it starts triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _attackController.StartAttacking();            
        }

        /// <summary>
        /// Stops triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_canceled(InputAction.CallbackContext obj)
        {
            _attackController.StopAttacking();            
        }

        private void SpecialAttack_performed(InputAction.CallbackContext obj)
        {
            if (_weaponsController.CanUseAOE)
                _weaponsController.UseAOE();
        }



        #endregion

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
            _animator.Play("Die");
            GameManager.Instance.ShowGameOverScreen();
        }

        #region HEALTH

        /// <summary>
        /// Reduces the character's health and trigger the correct animations
        /// </summary>
        /// <param name="points"></param>
        public void Damage(int points)
        {
            _healthPoints -= points;
            if (_healthPoints <= 0)
                Die();
            else Damage();
        }

        #endregion

        public Vector2 GetAttackPosition()
        {
            return _attackController.GetEnemyAttackPoint();
        }

        public void AddAttachedEnemy(IEnemyEffect enemyEffect)
        {
            _movementController.DecreaseSpeedBy(enemyEffect.SlowdownMultiplier);
        }

        public void RemoveAttachedEnemy(IEnemyEffect enemyEffect)
        {
            _movementController.IncreaseSpeedBy(enemyEffect.SlowdownMultiplier);
        }

        public void Push(Vector2 force)
        {
            _movementController.Push(force);
        }
    }

}

