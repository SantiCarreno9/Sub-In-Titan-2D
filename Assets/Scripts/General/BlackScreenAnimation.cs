using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BlackScreenAnimation : MonoBehaviour
{
    [SerializeField] private bool _playOnStart = false;
    [SerializeField] private Image _blackScreen;    
    [SerializeField] private float _animationDuration = 1;
        
    void Start()
    {
        if (_playOnStart)
        {
            _blackScreen.color = Color.black;
            FadeIn(_animationDuration);
        }
    }

    public void FadeIn(float duration = 1)
    {
        DOTween.To(() => _blackScreen.color, x => _blackScreen.color = x, new Color(0, 0, 0, 0), duration);        
    }

    public void FadeOut(float duration = 1)
    {
        DOTween.To(() => _blackScreen.color, x => _blackScreen.color = x, Color.black, duration);        
    }
}
