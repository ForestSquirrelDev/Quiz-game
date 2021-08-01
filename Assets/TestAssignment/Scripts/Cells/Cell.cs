using UnityEngine;

public class Cell : MonoBehaviour, ICell
{
    [SerializeField] private SpriteRenderer mainSpriteRenderer;

    public int Id { get; set; }

    public SpriteRenderer MainSpriteRenderer => mainSpriteRenderer;
}
