using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

namespace Submarine
{
    public class AnimationsController : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Movement")]
        [SerializeField] private MovementModule _movementController;
        [SerializeField] private VisualEffect[] _bubbles;
        [SerializeField] private Transform _bubblesCont;
        private int _arcSequencer = 0;
        private int _speedMultiplier = 0;

        [Header("Health")]
        [SerializeField] private HealthModule _healthModule;
        [SerializeField] private CanvasGroup _vignetteEffect;
        [SerializeField] private Color hurtColor = Color.red;
        private bool _isReceivingDamage = false;
        private bool _isPlayingLowHealthEffect = false;
        private Color _initialColor;
        private float _flickingTime = 15;

        private void Awake()
        {
            _initialColor = spriteRenderer.color;
        }        

        private void OnEnable()
        {
            _healthModule.OnDamageReceived += () => _isReceivingDamage = true;
            _healthModule.OnHealthRestored += PlayHealthRestoreAnimation;
            _healthModule.OnHealthChanged += UpdateAppearanceByHealth;
        }

        private void OnDisable()
        {
            _healthModule.OnDamageReceived -= () => _isReceivingDamage = true;
            _healthModule.OnHealthRestored -= PlayHealthRestoreAnimation;
            _healthModule.OnHealthChanged -= UpdateAppearanceByHealth;
        }


        // Start is called before the first frame update
        void Start()
        {
            _arcSequencer = Shader.PropertyToID("ArcSequencer");
            _speedMultiplier = Shader.PropertyToID("SpeedMultiplier");
        }

        private void FixedUpdate()
        {
            if (_isReceivingDamage)
                PlayDamageAnimation();
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
            float healthPercentage = ((float)healthPoints / (float)_healthModule.GetMaxHealth());
            if (healthPercentage < 0.25f)
            {
                if (!_isPlayingLowHealthEffect)
                {
                    DOTween.To(() => _vignetteEffect.alpha, x => _vignetteEffect.alpha = x, 0.1f, 2).SetLoops(-1, LoopType.Yoyo);
                    _isPlayingLowHealthEffect = true;
                }
            }
            else
            {
                DOTween.PauseAll();
                _vignetteEffect.alpha = 0;
                _isPlayingLowHealthEffect = false;
            }
        }

        private void PlayDamageAnimation()
        {
            spriteRenderer.color = spriteRenderer.color == _initialColor ? hurtColor : _initialColor;
            Debug.Log(spriteRenderer.color);
            _flickingTime--;
            if (_flickingTime < 0)
            {
                _isReceivingDamage = false; _flickingTime = 15;
                spriteRenderer.color = _initialColor;
            }
        }

        private void PlayHealthRestoreAnimation()
        {
            //Place logic to play animation
        }
    }
}