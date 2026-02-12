using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] TextMeshProUGUI _upgradeLevelText;

    private EconomyService _economyService;
    private UpgradeService _upgradeService;


    [Inject]
    public void Construct(EconomyService economyService, UpgradeService upgradeService)
    {
        _economyService = economyService;
        _upgradeService = upgradeService;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _economyService.Money
            .Subscribe(money =>
            {
                _button.interactable = money >= _upgradeService.CalculatePrice();
            })
            .AddTo(this);

        _upgradeService.CurrentUpgradeLevel
            .Subscribe(level =>
            {
                _upgradeLevelText.text = $"Уровень апгрейда: {level}";
                _priceText.text = $"Купить апгрейд за {_upgradeService.CalculatePrice()}";
            })
            .AddTo(this);

        _button.onClick.AddListener(() => _upgradeService.TryBuyUpgrade());
    }
}
