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

        [Header("Collision")]
        [SerializeField] private AudioSource _collisionAudioSource;
        [SerializeField] private AudioClip _collisionSound;        
        [Header("Attack")]
        [SerializeField] private AudioSource _attackAudioSource;
        [SerializeField] private AudioClip _basicAttackSound;
        [SerializeField] private AudioClip _aoeChargeSound;
        [SerializeField] private AudioClip _aoeFireSound;
        [SerializeField] private AudioClip _aoeCancelSound;
        [Header("Action Menu")]
        [SerializeField] private AudioSource _actionMenuAudioSource;        
        [SerializeField] private AudioClip _reloadingSound;
        [SerializeField] private AudioClip _fullReloadSound;
        [SerializeField] private AudioClip _repairingSound;
        [SerializeField] private AudioClip _fullRepairSound;
        [Header("Health")]
        [SerializeField] private AudioSource _healthAudioSource;
        [SerializeField] private AudioClip _lowHealthSound;

        [Space]
        [Header("Modules")]
        [SerializeField] private MovementModule _movementModule;
        [SerializeField] private AttackModule _attackModule;
        [SerializeField] private ActionMenuModule _actionMenuModule;
        [SerializeField] private HealthModule _healthModule;



        private void OnEnable()
        {               
            _attackModule.OnBasicAttackShot += PlayBasicAttackSound;
            _attackModule.OnAOECharge += PlayAOEChargeSound;
            _attackModule.OnAOECanceled += PlayAOECancelSound;
            _attackModule.OnAOEShot += PlayAOEFireSound;      
            
            _healthModule.OnHealthChanged += PlayHealthBasedSound;

            _actionMenuModule.ReloadController.OnProcessStarted += PlayReloadSound;
            _actionMenuModule.ReloadController.OnProcessCanceled += StopPlayingActionSounds;
            _actionMenuModule.ReloadController.OnProcessFinished += PlayReloadFinishSound;

            _actionMenuModule.RepairController.OnProcessStarted += PlayRepairSound;            
            _actionMenuModule.RepairController.OnProcessCanceled += StopPlayingActionSounds;            
            _actionMenuModule.RepairController.OnProcessFinished += PlayRepairFinishSound;            
        }

        private void OnDisable()
        {
            _attackModule.OnBasicAttackShot -= PlayBasicAttackSound;
            _attackModule.OnAOECharge -= PlayAOEChargeSound;
            _attackModule.OnAOECanceled -= PlayAOECancelSound;
            _attackModule.OnAOEShot -= PlayAOEFireSound;

            _healthModule.OnHealthChanged -= PlayHealthBasedSound;

            _actionMenuModule.ReloadController.OnProcessStarted -= PlayReloadSound;
            _actionMenuModule.ReloadController.OnProcessCanceled -= StopPlayingActionSounds;
            _actionMenuModule.ReloadController.OnProcessFinished -= StopPlayingActionSounds;

            _actionMenuModule.RepairController.OnProcessStarted -= PlayRepairSound;
            _actionMenuModule.RepairController.OnProcessCanceled -= StopPlayingActionSounds;
            _actionMenuModule.RepairController.OnProcessFinished -= StopPlayingActionSounds;
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

        #region ATTACK

        private void PlayBasicAttackSound() => _attackAudioSource.PlayOneShot(_basicAttackSound);
        private void PlayAOEChargeSound() => _attackAudioSource.PlayOneShot(_aoeChargeSound);
        private void PlayAOECancelSound()
        {
            if(_attackAudioSource.isPlaying)
                _attackAudioSource.Stop();
            _attackAudioSource.PlayOneShot(_aoeCancelSound);
        }

        private void PlayAOEFireSound() => _attackAudioSource.PlayOneShot(_aoeFireSound);

        #endregion

        private void PlayHealthBasedSound(int healthPoints)
        {
            float percentage = (float)healthPoints / (float)_healthModule.GetMaxHealth();
            if(percentage < 0.25f)
            {
                if (!_healthAudioSource.isPlaying)
                {
                    _healthAudioSource.clip = _lowHealthSound;
                    _healthAudioSource.Play();
                    _healthAudioSource.loop = true;
                }                
            }
            else
            {
                _healthAudioSource.Stop();
                _healthAudioSource.loop = false;
            }
        }

        #region ACTION MENU

        private void PlayRepairSound()
        {
            _actionMenuAudioSource.loop = true;
            _actionMenuAudioSource.clip = _repairingSound;
            _actionMenuAudioSource.Play();  
        }

        private void PlayRepairFinishSound() => _actionMenuAudioSource.PlayOneShot(_fullRepairSound);

        private void PlayReloadSound()
        {
            _actionMenuAudioSource.loop = true;
            _actionMenuAudioSource.clip = _reloadingSound;
            _actionMenuAudioSource.Play();
        }
        
        private void PlayReloadFinishSound() => _actionMenuAudioSource.PlayOneShot(_fullReloadSound);

        private void StopPlayingActionSounds()
        {
            _actionMenuAudioSource.Stop();
        }

        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _collisionAudioSource.Play();
        }
    }
}