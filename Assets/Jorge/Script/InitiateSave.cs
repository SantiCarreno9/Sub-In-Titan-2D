using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateSave : MonoBehaviour
{
    [Header("Sound Sources")]
    [SerializeField] private AudioSource audioSource;

    [Header("Save state")]
    [SerializeField] private bool isSaved = false;

    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isSaved)
        {
            SaveGame();
        }
    }

    private void SaveGame()
    {
        // not implemented Logic for saving
        isSaved = true;
        SaveTelegraph();
    }

    private void SaveTelegraph()
    {
        animator.SetBool("isSaving", true);
        SaveSoundEffect();
    }

    private void SaveSoundEffect()
    {
        audioSource.Play();
    }

    private void StopSFX() { animator.SetBool("isSaving", false); }
}
