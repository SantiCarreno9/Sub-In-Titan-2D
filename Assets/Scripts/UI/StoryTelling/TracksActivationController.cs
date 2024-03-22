using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

[System.Serializable]
public class Track
{
    public GameObject Object;
    public float Duration;
    public UnityEvent OnTrackPlayed;
}

public abstract class TracksActivationController : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroupRef;
    [SerializeField] protected Track[] Tracks;
    [SerializeField] protected float FadeInTime;
    [SerializeField] protected float FadeOutTime;
    [SerializeField] protected bool PlayOnStart;

    public UnityEvent OnCurrentTrackPlayed;


    private void Awake()
    {
        CanvasGroupRef.alpha = 0;
    }

    private void Start()
    {
        if (PlayOnStart)
            StartSequence();
    }

    public abstract void StartSequence();

    protected void FadeInTrack(Track track, Action callback = null)
    {
        track.Object.SetActive(true);
        DOTween.To(() => CanvasGroupRef.alpha, x => CanvasGroupRef.alpha = x, 1f, FadeInTime)
            .onComplete += () =>
            {
                track.OnTrackPlayed?.Invoke();
                OnCurrentTrackPlayed?.Invoke();
                callback?.Invoke();
            };
    }

    protected void FadeOutTrack(Track track, Action callback = null)
    {
        DOTween.To(() => CanvasGroupRef.alpha, x => CanvasGroupRef.alpha = x, 0f, FadeOutTime)
            .onComplete += () =>
            {
                track.Object.SetActive(false);
                callback?.Invoke();
            };
    }

}
