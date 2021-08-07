using UnityEngine;
using UnityEngine.EventSystems;

namespace QuizGame.Core
{
    /// <summary>
    /// Uses raycast to detect mouse clicks on cells. Clicking on UI does not count.
    /// </summary>
    public class MouseInput : MonoBehaviour, IPlayerInput
    {
        [SerializeField] private Camera cam;

        public event System.Action<int> OnCellWasClicked;

        private void Update() => HandleInput();

        public void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null
                    && !EventSystem.current.IsPointerOverGameObject())
                        OnCellWasClicked?.Invoke(hit.collider.gameObject.GetComponent<ICell>().Id);
            }
        }
    }
}