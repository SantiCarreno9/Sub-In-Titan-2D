using UnityEngine;
using UnityEngine.Events;

public class ControlledTracksActivationController : TracksActivationController
{
    [SerializeField] private GameObject _previousButton;
    [SerializeField] private GameObject _nextButton;

    public UnityEvent OnFirstTrackReached;
    public UnityEvent OnLastTrackReached;

    private Track _currentTrack = null;
    private sbyte _currentIndex = 0;

    public override void StartSequence()
    {
        CanvasGroupRef.alpha = 0;
        _currentIndex = -1;
        UpdateButtons();
        NextTrack();
    }

    public void PreviousTrack()
    {
        if (_currentIndex == 0)
            return;

        _currentIndex--;
        ChangeTracks();
    }

    public void NextTrack()
    {
        if (_currentIndex == Tracks.Length - 1)
            return;

        _currentIndex++;
        ChangeTracks();
    }

    private void ChangeTracks()
    {
        if (_currentTrack != null)
        {
            FadeOutTrack(_currentTrack, () =>
            {
                UpdateButtons();
                _currentTrack = null;
                ChangeTracks();
            });
            return;
        }

        _currentTrack = Tracks[_currentIndex];
        FadeInTrack(_currentTrack);
    }

    private void UpdateButtons()
    {
        if (_previousButton == null || _nextButton == null)
            return;

        if (_currentIndex <= 0)
        {
            _previousButton.SetActive(false);
            _nextButton.SetActive(true);
        }
        else if (_currentIndex == Tracks.Length - 1)
        {
            _previousButton.SetActive(true);
            _nextButton.SetActive(false);
        }
        else
        {
            _previousButton.SetActive(true);
            _nextButton.SetActive(true);
        }
    }
}
