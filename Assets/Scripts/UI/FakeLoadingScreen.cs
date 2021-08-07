using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace QuizGame.UI
{
    public class FakeLoadingScreen : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnFakeLoadingFinished;
        [SerializeField] private Image image;
        [SerializeField] private Image outline;

        [Range(0.01f, 1f)]
        [SerializeField] private float loadingTime = 1.0f;

        public void LoadGame() => StartCoroutine(LoadGameRoutine());

        private IEnumerator LoadGameRoutine()
        {
            image.gameObject.SetActive(true);
            outline.gameObject.SetActive(true);

            float counter = 0;

            while (counter <= loadingTime)
            {
                image.fillAmount = counter;
                counter += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            OnFakeLoadingFinished?.Invoke();

            image.gameObject.SetActive(false);
            outline.gameObject.SetActive(false);
        }
    }
}