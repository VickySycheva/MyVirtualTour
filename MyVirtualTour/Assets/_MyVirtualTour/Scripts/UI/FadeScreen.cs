using System;
using DG.Tweening;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;

    public void DoFadeIn(Action onStart = null, Action onEnd = null)
    {
        _canvasGroup.alpha = 0;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => onStart?.Invoke());
        sequence.Append(_canvasGroup.DOFade(1, _fadeDuration));
        sequence.AppendCallback(() => onEnd?.Invoke());
    }

    public void DoFadeOut(Action onStart = null, Action onEnd = null)
    {
        _canvasGroup.alpha = 1;

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => onStart?.Invoke());
        sequence.Append(_canvasGroup.DOFade(0, _fadeDuration));
        sequence.AppendCallback(() => onEnd?.Invoke());
    }
}
