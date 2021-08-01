using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Zenject;

/// <summary>
/// This MonoBehaviour stands for quiz logic.
/// Correct answer is attached to cell index so it's possible to pick any kind of sprite set.
/// </summary>
public class Quiz : MonoBehaviour
{
    [SerializeField] private UnityEvent OnQuizCompleted;
    [SerializeField] private UnityEvent<int> OnWrongAnswer;

    public event System.Action<string> OnCorrectAnswerGenerated;
    public event System.Action<int> OnCorrectAnswerGiven;

    [Tooltip("If this field is checked, random sprite List is chosen for every new answer.\nIf not, only the first one is picked.")]
    [SerializeField] bool useRandomSprites = true;

    [SerializeField] private List<SpriteContainer> spriteContainers;

    [Range(0.01f, 2.0f)]
    [Tooltip("How long to wait for bounce + particles animations to play.")]
    [SerializeField] private float animationWaitTime = 0.8f;

    private int currentSpriteContainer;

    private List<string> knownAnswers = new List<string> { };

    private bool suspending = false;

    // Used to generate correct answer.
    private int correctCellIndex;
    private string correctCellName;

    // Used to fill the grid.
    private int currentCellsCount;
    private int cellCountIncrement;

    private MouseInput mouseInput;
    private GridGenerator grid;
    private GridClearer gridClearer;

    [Inject]
    public void Construct(GridGenerator grid, GridClearer gridClearer, MouseInput mouseInput)
    {
        this.grid = grid;
        this.gridClearer = gridClearer;
        this.mouseInput = mouseInput;
    }

    private void Awake()
    {
        currentCellsCount = grid.RowLength;
        cellCountIncrement = grid.RowLength;
    }

    private void OnEnable() => mouseInput.OnCellWasClicked += CheckAnswer;

    private void OnDisable() => mouseInput.OnCellWasClicked -= CheckAnswer;

    public void GenerateNewQuestion()
    {
        if (useRandomSprites)
            currentSpriteContainer = Random.Range(0, spriteContainers.Count);
        else
            currentSpriteContainer = 0;

        // Avoiding Index out of range exception.
        if (spriteContainers[currentSpriteContainer].Sprites.Count < grid.Cells.Length)
        {
            Debug.LogWarning($"Length of container {currentSpriteContainer} is less than cells number. Can't proceed the game");
            return;
        }

        // Creating temporary List and further using it to avoid duplicates in the grid.
        var allSprites = new List<Sprite>(spriteContainers[currentSpriteContainer].Sprites);

        for (int i = currentCellsCount - 1; i >= 0; i--)
        {
            int randomNumber = Random.Range(0, allSprites.Count);
            Sprite tempSprite = allSprites[randomNumber];

            grid.Cells[i].gameObject.SetActive(true);
            grid.CellInfo[i].MainSpriteRenderer.sprite = tempSprite;

            allSprites.Remove(tempSprite);
        }

        GenerateCorrectAnswer();
    }

    /// <summary>
    /// Generating random cell index and checking if sprite's name on this cell is a known answer.
    /// If not, adding this name to the list of known answers.
    /// If the name is already known, generating new answer via recursion.
    /// </summary>
    private void GenerateCorrectAnswer()
    {
        var randomCellNum = Random.Range(0, currentCellsCount);
        var randomCellName = grid.CellInfo[randomCellNum].MainSpriteRenderer.sprite.name;
        
        if(!knownAnswers.Contains(randomCellName))
        {
            knownAnswers.Add(randomCellName);

            correctCellIndex = randomCellNum;
            correctCellName = randomCellName;

            OnCorrectAnswerGenerated?.Invoke(correctCellName);
        }
        else
        {
            GenerateCorrectAnswer();
        }
    }

    public void CheckAnswer(int answer) => StartCoroutine(CheckAnswerRoutine(answer));

    /// <summary>
    /// See whether the answer is correct and wait for animations to play.
    /// </summary>
    private IEnumerator CheckAnswerRoutine(int answer)
    {
        if (answer == correctCellIndex && !suspending)
        {
            currentCellsCount += cellCountIncrement;
            suspending = true;

            if (currentCellsCount > grid.Cells.Length)
            {
                currentCellsCount = grid.RowLength;
                knownAnswers = new List<string>();
                OnCorrectAnswerGiven?.Invoke(correctCellIndex);

                yield return new WaitForSeconds(animationWaitTime);

                suspending = false;
                OnQuizCompleted?.Invoke();
            }
            else
            {
                OnCorrectAnswerGiven?.Invoke(correctCellIndex);

                yield return new WaitForSeconds(animationWaitTime);

                gridClearer.ClearCells();
                suspending = false;
                GenerateNewQuestion();
            } 
        }
        else
        {
            OnWrongAnswer?.Invoke(answer);
            yield return new WaitForSeconds(animationWaitTime);
            suspending = false;
        }    
    }
}
