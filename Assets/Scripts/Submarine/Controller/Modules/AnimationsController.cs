using UnityEngine;
using UnityEngine.VFX;

namespace Submarine
{
    public class AnimationsController : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;

        [Header("Movement")]
        [SerializeField] private MovementModule _movementController;
        [SerializeField] private VisualEffect[] _bubbles;
        [SerializeField] private Transform _bubblesCont;
        private int _arcSequencer = 0;
        private int _speedMultiplier = 0;

        [Header("Health")]
        [SerializeField] private HealthModule _healthModule;


        private void OnEnable()
        {
            _healthModule.OnDamageReceived += PlayDamageAnimation;
            _healthModule.OnHealthRestored += PlayHealthRestoreAnimation;
            _healthModule.OnHealthChanged += UpdateAppearanceByHealth;
        }

        private void OnDisable()
        {
            _healthModule.OnDamageReceived -= PlayDamageAnimation;
            _healthModule.OnHealthRestored -= PlayHealthRestoreAnimation;
            _healthModule.OnHealthChanged -= UpdateAppearanceByHealth;
        }


        // Start is called before the first frame update
        void Start()
        {
            _arcSequencer = Shader.PropertyToID("ArcSequencer");
            _speedMultiplier = Shader.PropertyToID("SpeedMultiplier");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBubblesDirection();
        }

        private void UpdateBubblesDirection()
        {
            Vector2 movementDirection = _movementController.GetMovementDirection();
            bool isDashing = _movementController.IsDashing();
            float speedMultiplier;

            if (movementDirection.normalized.magnitude == 0)
                speedMultiplier = 0;
            else
            {
                float angle = Mathf.Atan2(-movementDirection.y, movementDirection.x) * Mathf.Rad2Deg - 90;
                if (angle < 0)
                    angle = 360 + angle;
                float percentage = angle / 360f;
                _bubbles[0].SetFloat(_arcSequencer, percentage);
                _bubbles[1].SetFloat(_arcSequencer, percentage);
                speedMultiplier = (isDashing) ? 1 : 0.5f;
            }

            _bubbles[0].SetFloat(_speedMultiplier, speedMultiplier);
            _bubbles[1].SetFloat(_speedMultiplier, speedMultiplier);
        }

        private void UpdateAppearanceByHealth(int healthPoints)
        {
            float healthPercentage = ((float)healthPoints / (float)_healthModule.GetMaxHealth()) * 100f;
            switch (healthPercentage)
            {
                case 25:
                    break;
                case 50:
                    break;
                case 75:
                    break;
                case 100:
                    break;
                default:
                    break;
            }
        }

        private void PlayDamageAnimation()
        {
            //Place logic to play animation            
        }

        private void PlayHealthRestoreAnimation()
        {
            //Place logic to play animation
        }
    }
}