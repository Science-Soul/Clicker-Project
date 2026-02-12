using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] GameConfig _gameConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameConfig).AsSingle();
        Container.BindInterfacesAndSelfTo<EconomyService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UpgradeService>().AsSingle().NonLazy();
    }
}
