using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class BlackScreenAnimation : MonoBehaviour
{
    [SerializeField] private bool _playFadeInOnStart = false;
    [SerializeField] private bool _playFadeOutOnStart = false;
    [SerializeField] private float _playDelay = 0.0f;
    [SerializeField] private Image _blackScreen;    
    [SerializeField] private float _animationDuration = 1;

    public UnityEvent OnFadedIn;
    public UnityEvent OnFadedOut;
        
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_playDelay);
        if (_playFadeInOnStart)
        {
            _blackScreen.color = Color.black;
            FadeIn(_animationDuration);
            yield return null;
        }

        if (_playFadeOutOnStart)
        {            
            FadeOut(_animationDuration);
            yield return null;
        }
    }

    public void FadeIn(float duration = 1)
    {
        DOTween.To(() => _blackScreen.color, x => _blackScreen.color = x, new Color(0, 0, 0, 0), duration)
            .onComplete += () => OnFadedIn?.Invoke();        
    }

    public void FadeOut(float duration = 1)
    {
        DOTween.To(() => _blackScreen.color, x => _blackScreen.color = x, Color.black, duration)
            .onComplete += () => OnFadedOut?.Invoke();
    }
}
