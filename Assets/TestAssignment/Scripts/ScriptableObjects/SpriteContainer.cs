using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SpriteContainer")]
public class SpriteContainer : ScriptableObject
{
    [SerializeField] private List<Sprite> sprites;

    public List<Sprite> Sprites => sprites;
}
