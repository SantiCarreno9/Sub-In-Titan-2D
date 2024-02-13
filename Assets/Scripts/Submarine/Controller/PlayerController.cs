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

        public Transform Transform => throw new System.NotImplementedException();

        private void Awake()
        {
            _inputs = new PlayerInputs();

            #region PLAYER ACTION MAP

            _inputs.Player.Move.performed += Move_performed;
            _inputs.Player.Move.canceled += Move_canceled;
            _inputs.Player.Dash.performed += Dash_performed;
            _inputs.Player.Dash.canceled += Dash_canceled;

            _inputs.Player.Aim.performed += Aim_performed;
            _inputs.Player.Fire.performed += Fire_performed;
            _inputs.Player.Fire.canceled += Fire_canceled;
            _inputs.Player.SpecialAttack.performed += SpecialAttack_performed;
            _inputs.Player.SpecialAttack.canceled += SpecialAttack_canceled;

            _inputs.Player.ActionMenu.performed += ActionMenu_performed;
            #endregion

            #region ACTION MENU ACTION MAP

            _inputs.ActionMenu.Reload.performed += Reload_performed;
            _inputs.ActionMenu.Repair.performed += Repair_performed;
            _inputs.ActionMenu.Cancel.performed += Cancel_performed;
            _inputs.ActionMenu.Quit.performed += Quit_performed;

            #endregion            
        }

        private void Start()
        {
            EnablePlayerActionMap();
            DisableActionMenuActionMap();
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

        #endregion

        #region ATTACK

        private void Aim_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _attackController.AimController.UpdateAimDirection(obj.ReadValue<Vector2>());
        }
        /// <summary>
        /// Ensures that the user is able to attack, if so it starts triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _attackController.StartBasicAttack();
        }

        /// <summary>
        /// Stops triggering the attack action
        /// </summary>
        /// <param name="obj"></param>
        private void Fire_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;
            _attackController.StopBasicAttack();
        }

        private void SpecialAttack_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _attackController.StartSpecialAttack();
        }

        private void SpecialAttack_canceled(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _attackController.StopSpecialAttack();
        }

        #endregion

        #region REPAIR & RELOAD

        private void ActionMenu_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _actionMenuController.Open();
            EnableActionMenuActionMap();
            DisablePlayerActionMap();
        }

        private void Repair_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _actionMenuController.StartRepairing();
        }

        private void Reload_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _actionMenuController.StartReloading();
        }

        private void Cancel_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _actionMenuController.CancelCurrentProcess();
        }


        private void Quit_performed(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.IsGamePaused())
                return;

            _actionMenuController.Close();
            DisableActionMenuActionMap();
            EnablePlayerActionMap();
        }
        #endregion

        #endregion

        private void EnablePlayerActionMap() => _inputs.Player.Enable();
        private void DisablePlayerActionMap() => _inputs.Player.Disable();

        private void EnableActionMenuActionMap() => _inputs.ActionMenu.Enable();
        private void DisableActionMenuActionMap() => _inputs.ActionMenu.Disable();


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
            _healthModule.Damage(amount);
        }

        public Vector2 GetRelativeAttackPosition()
        {
            return _attackController.GetEnemyAttackPoint();
        }
    }

}

