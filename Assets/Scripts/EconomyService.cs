using UniRx;
using Zenject;

public class EconomyService : IInitializable
{
    private readonly ReactiveProperty<long> _money = new ReactiveProperty<long>(0); // ≈сли даже long не хватит, то можно использовать BigInteger.
    public IReadOnlyReactiveProperty<long> Money => _money;
    private readonly GameConfig _gameConfig;

    public EconomyService(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    public void Initialize()
    {
        //_money.Value = _gameConfig.InitialMoney;
    }

    public void AddMoney(long amount)
    {
        if (amount <= 0) return;
        _money.Value += amount;
    }

    public bool TrySpendMoney(long amount)
    {
        if (amount <= 0 || _money.Value < amount)
        {
            return false;
        }

        _money.Value -= amount;
        return true;
    }
}
