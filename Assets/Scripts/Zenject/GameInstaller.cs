using Zenject;
using UnityEngine;
using QuizGame.Core;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GridGenerator grid;
    [SerializeField] private GridClearer gridClearer;
    [SerializeField] private Quiz quiz;
    [SerializeField] private MouseInput mouseInput;

    public override void InstallBindings() => BindCoreSystems();

    private void BindCoreSystems()
    {
        Container.Bind<GridGenerator>().FromInstance(grid).AsCached();
        Container.Bind<GridClearer>().FromInstance(gridClearer).AsCached();
        Container.Bind<Quiz>().FromInstance(quiz).AsCached();
        Container.Bind<MouseInput>().FromInstance(mouseInput).AsCached();
    }
}
