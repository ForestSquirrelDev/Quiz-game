using UnityEngine;
using System.Collections;
using DG.Tweening;

/// <summary>
/// Applies bounce effect to the whole prefab or given sprite transform.
/// </summary>
public class Bounce : MonoBehaviour
{
    [Header("'Bounce' tweaking values.")]

    [Range(0.01f, 1.0f)]
    [Tooltip("Mild scale time before edgy punching scale.")]
    [SerializeField] private float scaleDuration = 0.2f;

    [Range(0.01f, 1.0f)]
    [Tooltip("How much seconds to wait before using punchScale (only for prefab punching).")]
    [SerializeField] private float waitTime = 0.11f;

    [Tooltip("Default scale of sprite prefab before any manipulations.")]
    [SerializeField] private Vector3 startScale = Vector3.zero;

    [Tooltip("Strength of punchScale")]
    [SerializeField] private Vector3 punchScale = new Vector3(.7f, .7f, 0);
    
    [Range(0.01f, 2.0f)]
    [Tooltip("Length of 'bounce' effect")]
    [SerializeField] private float punchDuration = 0.99f;

    [Range(1,10)]
    [Tooltip("Amount of craziness for bounce")]
    [SerializeField] private int punchVibrato = 6;

    [Range(0.01f, 1.0f)]
    [SerializeField] private float punchElasticity = 1.0f;

    private float targetScale = 1.2f;

    private GridGenerator grid;
    private Quiz quiz;
    private GameObject[] cells;
    
    [Zenject.Inject]
    public void Construct(GridGenerator grid, Quiz quiz)
    {
        this.grid = grid;
        this.quiz = quiz;
    }

    private void OnEnable() => quiz.OnCorrectAnswerGiven += DOLocalBounce;

    private void OnDisable() => quiz.OnCorrectAnswerGiven -= DOLocalBounce;

    /// <summary>
    /// Bounce the row of gameObjects on start of the game.
    /// </summary>
    public void DOGeneralBounce()
    {
        targetScale = grid.CellScale;
        cells = grid.Cells;

        for(int i = 0; i < cells.Length; i++)
            if(cells[i].gameObject.activeSelf)
                StartCoroutine(ApplyBounce(targetScale, cells[i].transform, startScale));
    }

    /// <summary>
    /// Bounce only main sprite, ignoring template prefab.
    /// </summary>
    public void DOLocalBounce(int id) => StartCoroutine(ApplyLocalBounce(id));

    private IEnumerator ApplyLocalBounce(int id)
    {
        Transform t = grid.CellInfo[id].MainSpriteRenderer.transform;

        var startScale = t.localScale;
        t.DOPunchScale(startScale, punchDuration, punchVibrato, 1f);

        yield return new WaitForSeconds(punchDuration * 0.99f);

        t.DOScale(startScale, 2.5f);
    }

    private IEnumerator ApplyBounce(float targetScale, Transform target, Vector3 startScale) 
    {
        target.localScale = startScale;
        target.DOScale(targetScale, scaleDuration);

        yield return new WaitForSeconds(waitTime);

        target.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity);

        yield return new WaitForSeconds(punchDuration);

        target.DOScale(targetScale, 10f);
    }
}
