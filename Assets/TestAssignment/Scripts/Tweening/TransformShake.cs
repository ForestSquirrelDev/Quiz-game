using UnityEngine;
using DG.Tweening;

/// <summary>
/// Shakes position of sprite transform on the wrong answer.
/// </summary>
public class TransformShake : MonoBehaviour
{
    [Range(0.01f, 3.0f)]
    [SerializeField] float duration = 2.0f;

    [Range(0f, 90.0f)]
    [SerializeField] float randomness = 0f;

    [Range(1, 10)]
    [SerializeField] private int vibrato = 5;

    [SerializeField] Vector3 strength = new Vector3(0.2f, 0f, 0f);

    private GridGenerator grid;

    [Zenject.Inject]
    public void Construct(GridGenerator grid) => this.grid = grid;

    public void DOShakePos(int id)
    {
        Transform target = grid.CellInfo[id].MainSpriteRenderer.transform;
        target.DOShakePosition(duration, strength, vibrato, randomness, snapping: false, fadeOut: true);
    }
}
