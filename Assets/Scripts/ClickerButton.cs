using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickerButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] long _clickPower = 1; // Здесь тоже используем лонг.

    [SerializeField] float _animationDuration = 0.1f;
    [SerializeField] float _punchScale = 0.1f;

    private EconomyService _economyService;

    [Inject]
    public void Construct(EconomyService economyService)
    {
        _economyService = economyService;
    }

    private void Awake()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        _economyService.AddMoney(_clickPower);
        transform.DOPunchScale(Vector3.one * _punchScale, _animationDuration);
    }
}
