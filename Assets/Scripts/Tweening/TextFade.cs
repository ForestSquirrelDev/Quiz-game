using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QuizGame.Tweening
{
    /// <summary>
    /// Applies short text fade in the beggining and at the end of quiz.
    /// </summary>
    public class TextFade : MonoBehaviour
    {
        [Range(0.1f, 2.0f)]
        [SerializeField] private float fadeInLength = 1.0f;

        [Range(0.1f, 2.0f)]
        [SerializeField] private float fadeOutLength = 0.5f;

        [SerializeField] private Text text;

        private void Start() => FadeTextIn();

        public void FadeTextIn()
        {
            Sequence fadeSeq = DOTween.Sequence();

            fadeSeq.Append(text.DOFade(1.0f, fadeInLength));
        }

        public void FadeTextOut()
        {
            Sequence fadeSeq = DOTween.Sequence();

            fadeSeq.Append(text.DOFade(0f, fadeOutLength));
        }
    }
}