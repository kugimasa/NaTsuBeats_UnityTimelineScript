using DG.Tweening;
using UnityEngine;

public class CustomDirector : MonoBehaviour
{
    [SerializeField] private CanvasGroup _uiGroup;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BackgroundMover _backgroundMover;

    public void ResetBackground()
    {
        _backgroundMover.Reset();
    }

    public void Fade(float t)
    {
        _uiGroup.alpha = Mathf.Lerp(0, 1, t);
        _audioSource.volume = Mathf.Lerp(0.5f, 0.0f, t);
    }
}