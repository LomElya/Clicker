using TMPro;
using UnityEngine;
using Zenject;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _value;

    private Wallet _wallet;

    [Inject]
    public void Init(Wallet wallet)
    {
        _wallet = wallet;
        
        UpdateValue(_wallet.GetCurrentCoins());

        _wallet.CoinsChanged += UpdateValue;
    }

    private void OnDestroy() => _wallet.CoinsChanged -= UpdateValue;

    private void UpdateValue(int value) => _value.text = value.ToString();
}
