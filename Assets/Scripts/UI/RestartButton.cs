using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

namespace QuizGame.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnLevelRestarted;
        [SerializeField] private Button restartButton;
        [SerializeField] private Image blackScreen;

        public void EnableRestartButton()
        {
            restartButton.gameObject.SetActive(true);
            restartButton.image.DOFade(1.0f, 1.5f);
        }

        public void RestartLevel() => StartCoroutine(RestartLevelRoutine());

        private IEnumerator RestartLevelRoutine()
        {
            restartButton.image.DOFade(0f, 0.5f);

            yield return new WaitForSeconds(0.5f);

            restartButton.gameObject.SetActive(false);
            OnLevelRestarted?.Invoke();
        }
    }
}