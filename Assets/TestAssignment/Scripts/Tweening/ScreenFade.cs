using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// Manages screen fading via canvas image.
/// </summary>
public class ScreenFade : MonoBehaviour
{
    [Tooltip("Image that will be used as fade-out.")]
    [SerializeField] private Image blackScreen;

    [Tooltip("Duration of fade-out and fade-in effects.")]
    [Range(0.1f, 5.0f)]
    [SerializeField] private float duration = 3.0f;

    [Range(0.1f, 1.0f)]
    [Tooltip("Target alpha value to tween into. Used for fade-out effect only.")]
    [SerializeField] private float alpha = 1.0f;

    public void FadeIn()
    {
        StartCoroutine(FadeIn(duration));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(duration, alpha));
    }

    private IEnumerator FadeIn(float duration = 2.0f, float alpha = 0f)
    {
        blackScreen.DOFade(alpha, duration);
        yield return new WaitForSeconds(duration);
        blackScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(float duration = 2.0f, float alpha = 1.0f)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(alpha, duration);
        yield return new WaitForSeconds(duration);
    }
}
