using UnityEngine;
using UnityEngine.InputSystem;

namespace Submarine
{
    public class PlayerController : MonoBehaviour, ISubmarine
    {
        [SerializeField] private MovementModule _movementController;
        [SerializeField] private AttackModule _attackController;
        [SerializeField] private HealthModule _healthModule;
        [SerializeField] private ActionMenuModule _actionMenuController;

        private PlayerInputs _inputs;
        private bool _allowUserInputs = true;                  

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
            _inputs.Player.SpecialAttack.canceled += SpecialAttack_canceled;

            _inputs.Player.ActionMenu.performed += ActionMenu_performed;
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

        #region MOVEMENT
        /// <summary>
        /// Reads the movement input and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _movementController.SetUserMovementInput(obj.ReadValue<Vector2>());
        }

        /// <summary>
        /// Resets the movement Vector and updates the avatar orientation
        /// </summary>
        /// <param name="obj"></param>
        private void Move_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _movementController.SetUserMovementInput(Vector2.zero);
        }

        private void Dash_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _movementController.StartDashing();
        }

        private void Dash_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _movementController.StopDashing();
        }

        #endregion

        #region ATTACK
        /// <summary>
        /// Ensures that the user is able to attack, if so it starts triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _attackController.StartBasicAttack();
        }

        /// <summary>
        /// Stops triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;
            _attackController.StopBasicAttack();
        }

        private void SpecialAttack_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _attackController.StartSpecialAttack();
        }

        private void SpecialAttack_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused() || !_allowUserInputs)
                return;

            _attackController.StartSpecialAttack();
        }

        #endregion

        private void ActionMenu_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            if (!_actionMenuController.IsOpen)
            {
                _actionMenuController.Open();
                DisableUserInputs();
                _attackController.DisableModule();
                _movementController.DisableModule();
            }
            else
            {
                _actionMenuController.Close();
                _attackController.EnableModule();
                _movementController.EnableModule();
                EnableUserInputs();
            }
        }

        private void EnableUserInputs() => _allowUserInputs = true;
        private void DisableUserInputs() => _allowUserInputs = false;

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

        public void Damage(int amount)
        {
            throw new System.NotImplementedException();
        }
    }

}

