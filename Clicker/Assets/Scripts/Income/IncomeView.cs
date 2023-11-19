using TMPro;
using UnityEngine;
using Zenject;

public class IncomeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;
    [SerializeField] private string _template;

    private int _currentIncome = 0;

    private ClickableItemView _clickableItemView;

    private Income _income;
    private Wallet _wallet;

    [Inject]
    public void Init(Income income, Wallet wallet, ClickableItemView clickableItemView)
    {
        _income = income;
        _wallet = wallet;

        _clickableItemView = clickableItemView;

        _currentIncome = _income.GetCurrentIncome();
        UpdateValue(_currentIncome);

        InvokeRepeating(nameof(PayIncome), 1f, 1f);
        _income.IncomeChanged += UpdateValue;
    }

    private void OnDestroy() => _income.IncomeChanged -= UpdateValue;

    private void UpdateValue(int value)
    {
        _currentIncome = value * _clickableItemView.Multiplier;
        _value.text = string.Format(_template, _currentIncome);
    }

    private void PayIncome() =>
        _wallet.AddCoin(_currentIncome);
}
