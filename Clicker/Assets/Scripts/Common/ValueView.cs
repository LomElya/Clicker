using System;
using TMPro;
using UnityEngine;

public class ValueView<T> : MonoBehaviour where T : IConvertible
{
    [SerializeField] protected TMP_Text _text;

    public virtual void Show(T value)
    {
        gameObject.SetActive(true);
        _text.text = value.ToString();
    }

    public virtual void Calculate(T value) => _text.text = value.ToString();

    public void Hide() => gameObject.SetActive(false);
}
