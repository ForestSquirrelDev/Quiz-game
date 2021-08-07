using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using QuizGame.Core;

namespace QuizGame.Tweening
{
    /// <summary>
    /// Applies bounce effect to the whole prefab or given sprite transform.
    /// </summary>
    public class Bounce : MonoBehaviour
    {
        [Header("Bounce tweaking values.")]

        [Range(0.01f, 1.0f)]
        [Tooltip("Mild scale time before edgy punching scale.")]
        [SerializeField] private float scaleDuration = 0.106f;

        [Range(0.01f, 1.0f)]
        [Tooltip("How much seconds to wait before using punchScale (only for prefab punching).")]
        [SerializeField] private float waitTime = 0.101f;

        [Tooltip("Default scale of sprite prefab before any manipulations.")]
        [SerializeField] private Vector3 startScale = Vector3.zero;

        [Tooltip("Strength of punchScale")]
        [SerializeField] private Vector3 punchScale = new Vector3(.7f, .7f, 0);

        [Range(0.01f, 2.0f)]
        [Tooltip("Length of 'bounce' effect")]
        [SerializeField] private float punchDuration = 0.811f;

        [Range(1, 10)]
        [Tooltip("Amount of craziness for bounce")]
        [SerializeField] private int punchVibrato = 4;

        [Range(0.01f, 1.0f)]
        [Tooltip("DOTween: 'Represents how much the vector will go beyond the starting size when bouncing backwards.'\nNot advised to tweak.")]
        [SerializeField] private float punchElasticity = 1.0f;

        private List<int> runningTweens = new List<int> { };

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
        /// Bounce the row of gameObjects at start of the game.
        /// </summary>
        public void DOGeneralBounce()
        {
            targetScale = grid.CellScale;
            cells = grid.Cells;

            for (int i = 0; i < cells.Length; i++)
                if (cells[i].gameObject.activeSelf)
                    ApplyGeneralBounce(targetScale, cells[i].transform, startScale);
        }

        private void ApplyGeneralBounce(float targetScale, Transform target, Vector3 startScale)
        {
            target.localScale = startScale;

            Sequence bounceSeq = DOTween.Sequence();

            bounceSeq.Append(target.DOScale(targetScale, scaleDuration));
            bounceSeq.Insert(waitTime, target.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity));
            bounceSeq.Insert(waitTime + punchDuration, target.DOScale(targetScale, 10f));
        }

        /// <summary>
        /// Bounce only main sprite, ignoring template prefab.
        /// </summary>
        public void DOLocalBounce(int id)
        {
            if (!runningTweens.Contains(id))
            {
                runningTweens.Add(id);

                Transform target = grid.CellInfo[id].MainSpriteRenderer.transform;
                Vector3 startScale = target.localScale;

                Sequence bounceSeq = DOTween.Sequence();

                bounceSeq.Append(target.DOPunchScale(startScale, punchDuration, punchVibrato, punchElasticity));
                bounceSeq.Insert(punchDuration * .99f, target.DOScale(startScale, 2.5f));
                bounceSeq.InsertCallback(punchDuration * .99f,
                                         () => runningTweens.Remove(id));
            }
        }
    }
}