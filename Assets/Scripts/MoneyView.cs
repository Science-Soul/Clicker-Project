using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class MoneyView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] float _animationDuration = 0.2f;
    [SerializeField] float _punchScale = 0.1f;

    private EconomyService _economyService;
    private Tween _currentTween;

    [Inject] // Здесь нужно именно внедрение, потому что в монобехах нельзя использовать конструктор (компонент создается движком Юнити).
    public void Construct(EconomyService economyService)
    {
        _economyService = economyService;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _economyService.Money
            .Subscribe(OnMoneyChanged)
            .AddTo(this);
    }

    private void OnMoneyChanged(long newBalance)
    {
        _moneyText.text = $"Деньги: {newBalance}";

        _currentTween?.Kill(true);
        _currentTween = _moneyText.transform.DOPunchScale(Vector3.one * _punchScale, _animationDuration);
    }
}
