using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Creates grid of set size and cell scale at the center of screen.
/// </summary>
public class GridGenerator : MonoBehaviour, IGrid
{
    [SerializeField] private UnityEvent OnGridCreated;

    [Tooltip("Background prefab of cell.")]
    [SerializeField] private GameObject cellTemplate;

    [Range(1, 6)]
    [Tooltip("Number of cells in each row. Quiz will adjust to the length of row and number of columns.")]
    [SerializeField] private int rowLength = 3;

    [Range(1,6)]
    [Tooltip("Number of columns in the grid.")]
    [SerializeField] private int columnCount = 3;

    [Tooltip("Scale of each tile (not prefab).")]
    [SerializeField] private float tileScale = 1.2f;

    [Tooltip("Scale of each cell (prefab).")]
    [SerializeField] private float cellScale = 1.2f;

    public GameObject[] Cells { get; private set; }
    public ICell[] CellInfo { get; private set; }

    public int RowLength => rowLength;
    public int ColumnCount => columnCount;
    public float CellScale => cellScale;

    private void Start() => CreateGrid();

    public void CreateGrid()
    {
        // Caching two arrays to have an easy access to any cell data and avoid excessive usage of GetComponent<T>().
        Cells = new GameObject[rowLength * columnCount];
        CellInfo = new ICell[rowLength * columnCount];

        int iteration = 0;

        for(int i = 0; i < columnCount; i++)
            for(int j = 0; j < rowLength; j++)
            {
                GameObject cell = Instantiate(original: cellTemplate, parent: this.transform);

                float posX = j * tileScale;
                float posY = i * -tileScale;

                Cells[iteration] = cell;
                CellInfo[iteration] = cell.GetComponent<ICell>();

                CellInfo[iteration].Id = iteration;

                cell.transform.localScale = new Vector3(cellScale, cellScale, cellScale);

                iteration++;

                cell.transform.position = new Vector2(posX, posY);
            }

        float gridWidth = columnCount * tileScale;
        float gridHeight = rowLength * tileScale;

        Vector3 centerOfScreen = new Vector3(x: -gridHeight / 2 + tileScale / 2,
                                             y: gridWidth / 2 - tileScale / 2,
                                             z: 0.5f); // Placing grid behind the background image to add fancy transparency effect.

        transform.position = centerOfScreen;

        OnGridCreated?.Invoke();
    }

    public void ResetTransform() => transform.position = Vector3.zero;
}
