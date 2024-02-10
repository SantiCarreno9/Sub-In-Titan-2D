using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarine
{
    public class SubmarineSoundEffectsController : MonoBehaviour
    {
        [Header("Audios")]
        [Header("Movement")]
        [SerializeField] private AudioSource _movementAudioSource;
        [SerializeField] private AudioClip _idleSound;
        [SerializeField] private AudioClip _movementSound;
        private bool _isMoving;

        [Header("Attack")]
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private AudioClip _basicAttackSound;
        [SerializeField] private AudioClip _aoeChargeSound;
        [SerializeField] private AudioClip _aoeFireSound;
        [SerializeField] private AudioClip _aoeCancelSound;
        [Header("Action Menu")]
        [SerializeField] private AudioSource _actionMenuAudioSource;
        [SerializeField] private AudioClip _cancelSound;
        [SerializeField] private AudioClip _reloadingSound;
        [SerializeField] private AudioClip _fullReloadSound;
        [SerializeField] private AudioClip _repairingSound;
        [SerializeField] private AudioClip _fullrepairSound;
        [Header("Health")]
        [SerializeField] private AudioSource _healthAudioSource;
        [SerializeField] private AudioClip _damageSound;

        [Space]
        [Header("Modules")]
        [SerializeField] private MovementModule _movementModule;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private ActionMenuModule _actionMenuModule;
        [SerializeField] private HealthModule _healthModule;



        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PlayMovementSounds();
        }

        private void PlayMovementSounds()
        {
            //if ()
        }
    }
}