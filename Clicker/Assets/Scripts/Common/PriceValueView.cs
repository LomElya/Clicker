using UnityEngine;
using Zenject;

public class PriceValueView : IntValueView
{
    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unlockColor;

    private bool _isLock;

    private Wallet _wallet;

    private void OnDestroy()
    {
        //_wallet.CoinsChanged -= ChangeValue;
    }

    /*  [Inject]
     public void InitWallet(Wallet wallet)
     {
         _wallet = wallet;
         _wallet.CoinsChanged += ChangeValue;
     } */

    private void Lock()
    {
        _isLock = true;
        _text.color = _lockColor;
    }

    private void Unlock()
    {
        _isLock = false;
        _text.color = _unlockColor;
    }

    private void ChangeValue(int value)
    {
        if (_wallet.IsEnough(_value))
            Unlock();
        else
            Lock();
    }
}
