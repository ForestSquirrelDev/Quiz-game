using UnityEngine;
using DG.Tweening;

/// <summary>
/// Shakes position of sprite transform on the wrong answer.
/// </summary>
public class TransformShake : MonoBehaviour
{
    private GridGenerator grid;

    [Zenject.Inject]
    public void Construct(GridGenerator grid) => this.grid = grid;

    public void DOShakePos(int id)
    {
        Transform target = grid.CellInfo[id].MainSpriteRenderer.transform;
        target.DOShakePosition(2.0f,
                          strength: new Vector3(.2f, 0, 0),
                          vibrato: 5,
                          randomness: 0,
                          snapping: false,
                          fadeOut: true);
    }
}
