using UniRx;
using Zenject;
using System;
using UnityEngine;

public class UpgradeService : IInitializable, IDisposable
{
    private readonly GameConfig _gameConfig;
    private readonly EconomyService _economyService;
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    //private long _passiveIncome = 5;

    public IntReactiveProperty CurrentUpgradeLevel = new IntReactiveProperty(0);

    public UpgradeService(EconomyService economyService, GameConfig gameConfig)
    {
        _economyService = economyService;
        _gameConfig = gameConfig;
    }

    public void Initialize()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => CurrentUpgradeLevel.Value > 0)
            .Subscribe(_ =>
            {
                long income = _gameConfig.BasePassiveIncome * CurrentUpgradeLevel.Value;
                _economyService.AddMoney(income);
            })
            .AddTo(_disposables);
    }

    public void TryBuyUpgrade()
    {
        long price = CalculatePrice();

        if (_economyService.TrySpendMoney(price))
        {
            CurrentUpgradeLevel.Value++;
            Debug.Log("Куплен новый апгрейд!");
        }
    }
    public long CalculatePrice()
    {
        return _gameConfig.BaseUpgradePrice * (CurrentUpgradeLevel.Value + 1);
    }
    public void Dispose()
    {
        _disposables.Dispose();
    }
}
