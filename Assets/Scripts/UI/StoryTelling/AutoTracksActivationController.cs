using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AutoTracksActivationController : TracksActivationController
{
    public UnityEvent OnSequenceStarted;
    public UnityEvent OnSequenceFinished;
    private byte _currentIndex = 0;

    public override void StartSequence()
    {
        CanvasGroupRef.alpha = 0;
        OnSequenceStarted?.Invoke();
        StartCoroutine(PlaySequenceCoroutine());
    }

    private IEnumerator PlaySequenceCoroutine()
    {
        while (_currentIndex < Tracks.Length)
        {
            yield return StartCoroutine(PlayTrack(Tracks[_currentIndex]));
            _currentIndex++;
        }
        OnSequenceFinished?.Invoke();
    }

    private IEnumerator PlayTrack(Track track)
    {
        FadeInTrack(track);
        yield return new WaitForSeconds(track.Duration + FadeInTime);
        FadeOutTrack(track);
        yield return new WaitForSeconds(FadeOutTime);
    }
}
