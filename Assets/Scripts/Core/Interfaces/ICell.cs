using UnityEngine;

namespace QuizGame.Core
{
    public interface ICell
    {
        public int Id { get; set; }
        public SpriteRenderer MainSpriteRenderer { get; }
    }
}