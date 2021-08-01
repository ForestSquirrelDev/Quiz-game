using UnityEngine;
using Zenject;

/// <summary>
/// Disables or destroys all cells.
/// </summary>
public class GridClearer : MonoBehaviour
{
    private GridGenerator grid;

    [Inject]
    public void Construct(GridGenerator grid) => this.grid = grid;

    public void ClearCells()
    {
        for (int i = 0; i < grid.Cells.Length; i++)
            grid.Cells[i].SetActive(false);
    }

    public void DestroyCells()
    {
        foreach(GameObject obj in grid.Cells)
        {
            Destroy(obj);
        }
    }
}
