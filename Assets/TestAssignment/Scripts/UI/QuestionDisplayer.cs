using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays current answer. Name of the target is picked from sprite name.
/// </summary>
public class QuestionDisplayer : MonoBehaviour
{
    [SerializeField] private Text text;

    private Quiz quiz;

    [Zenject.Inject]
    public void Construct(Quiz quiz) => this.quiz = quiz;

    private void OnEnable() => quiz.OnCorrectAnswerGenerated += DisplayText;

    private void OnDisable() => quiz.OnCorrectAnswerGenerated -= DisplayText;

    public void DisplayText(string text) => this.text.text = "Find " + text;
}
