using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace QuizGame.Tweening
{
    /// <summary>
    /// Manages screen fading via canvas image.
    /// </summary>
    public class ScreenFade : MonoBehaviour
    {
        [Tooltip("Duration of fade-out and fade-in effects.")]
        [Range(0.1f, 5.0f)]
        [SerializeField] private float duration = 3.0f;

        [Range(0.1f, 1.0f)]
        [Tooltip("Target alpha value to tween into. Used for fade-out effect only.")]
        [SerializeField] private float alpha = 1.0f;

        [Tooltip("Image used for fading.")]
        [SerializeField] private Image blackScreen;

        public void FadeIn()
        {
            Sequence fadeSeq = DOTween.Sequence();
            TweenCallback disabler = DisableScreen;

            fadeSeq.Append(blackScreen.DOFade(0f, duration));
            fadeSeq.InsertCallback(duration, disabler);
        }

        public void FadeOut()
        {
            Sequence fadeSeq = DOTween.Sequence();
            TweenCallback enabler = EnableScreen;

            fadeSeq.AppendCallback(enabler);
            fadeSeq.Append(blackScreen.DOFade(alpha, duration));
        }

        private void EnableScreen() => blackScreen.gameObject.SetActive(true);

        private void DisableScreen() => blackScreen.gameObject.SetActive(false);
    }
}