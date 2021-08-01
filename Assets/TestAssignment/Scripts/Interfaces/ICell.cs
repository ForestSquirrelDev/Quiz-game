using UnityEngine;

public interface ICell
{
    public int Id { get; set; }
    public SpriteRenderer MainSpriteRenderer { get; }
}
