using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class FakeLoadingScreen : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFakeLoadingFinished;
    [SerializeField] private Image image;
    [SerializeField] private Image outline;
    [SerializeField] private float loadingTime = 1.5f;

    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine());
    }

    private IEnumerator LoadGameRoutine()
    {
        image.gameObject.SetActive(true);
        outline.gameObject.SetActive(true);

        float i = loadingTime;

        while(i > 0)
        {
            image.fillAmount = i;
            i -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        OnFakeLoadingFinished?.Invoke();

        image.gameObject.SetActive(false);
        outline.gameObject.SetActive(false);
    }
}
