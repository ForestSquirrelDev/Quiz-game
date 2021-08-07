using QuizGame.Core;
using UnityEngine;

namespace QuizGame.VFX
{
    /// <summary>
    /// Emits particles from the position of given cell.
    /// </summary>
    public class StarParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private Vector3 offset = Vector3.zero;

        private Quiz quiz;
        private GridGenerator grid;

        [Zenject.Inject]
        public void Construct(Quiz quiz, GridGenerator grid)
        {
            this.quiz = quiz;
            this.grid = grid;
        }

        private void OnEnable() => quiz.OnCorrectAnswerGiven += PlayParticles;

        private void OnDisable() => quiz.OnCorrectAnswerGiven -= PlayParticles;

        public void PlayParticles(int id)
        {
            transform.position = grid.Cells[id].transform.position + offset;
            particles.Play();
        }
    }
}