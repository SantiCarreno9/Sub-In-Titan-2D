using UnityEngine;
using UnityEngine.UI;

namespace Submarine
{
    public class MovementModule : BaseModule
    {
        [Header("Physics")]
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Movement Specs")]
        [SerializeField] private float _normalSpeed = 7f;
        [SerializeField] private float _linearDrag = 1.5f;

        [Header("Dash")]
        [SerializeField] private float _dashSpeed = 12f;
        [SerializeField] private float _dashDuration = 2.0f;
        [SerializeField] private float _dashRecoveryRate = 0.5f;

        private float _dashRemainingTime = 0;
        private bool _isDashing = false;

        private Vector2 _movementInputs;
        private float _speed = 0;
        private float _speedMultiplier = 1f;
        private Vector2 _movement;


        //TEST ONLY
        [Space]
        [SerializeField] private Image _dashBar;

        private void Awake()
        {
            _speed = _normalSpeed;
            _rigidbody.drag = _linearDrag;
            _dashRemainingTime = _dashDuration;
        }

        private void FixedUpdate()
        {
            if (!IsEnabled)
                return;

            Move();

            UpdateDashUsage();
            _dashBar.fillAmount = GetRemainingDashPercentage();
        }

        public override void EnableModule()
        {
            base.EnableModule();
        }

        public override void DisableModule()
        {
            base.DisableModule();
        }

        public float GetDashUsage()
        {
            return _dashRemainingTime;
        }

        public void SetUserMovementInput(Vector2 movementInputs)
        {
            _movementInputs = movementInputs;
        }

        #region DASH

        public void StartDashing() => _isDashing = true;

        public void StopDashing() => _isDashing = false;

        private void UpdateDashUsage()
        {
            if (_isDashing)
            {
                if (_dashRemainingTime > 0)
                    _dashRemainingTime -= Time.fixedDeltaTime;
                else _dashRemainingTime = 0;
            }
            else
            {
                if (_dashRemainingTime < _dashDuration)
                    _dashRemainingTime += Time.fixedDeltaTime * _dashRecoveryRate;
                else _dashRemainingTime = _dashDuration;
            }
        }

        private float GetRemainingDashPercentage()
        {
            return _dashRemainingTime / _dashDuration;
        }

        private bool CanDash() => _dashRemainingTime > 0;

        #endregion

        public Vector2 GetVelocity() => _rigidbody.velocity;

        private void Move()
        {
            if (_isDashing && CanDash())
                _speed = _dashSpeed;
            else _speed = _normalSpeed;

            _movement = _movementInputs * _speed;
            Push(_movement);
        }

        public void Push(Vector2 forceVector)
        {
            _rigidbody.AddForce(forceVector, ForceMode2D.Force);
        }

        public void DecreaseSpeedBy(float multiplier)
        {
            _speedMultiplier -= multiplier;
            if (_speedMultiplier < 0)
                _speedMultiplier = 0;
        }

        public void IncreaseSpeedBy(float multiplier)
        {
            _speedMultiplier += multiplier;
            if (_speedMultiplier > 1)
                _speedMultiplier = 1;
        }
    }
}