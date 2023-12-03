using TMPro;
using UnityEngine;

public class StringValueView : ValueView<string>
{
    [SerializeField] private string _template;

    public TMP_Text CurrentText => _text;

    public override void Show(string value)
    {
        gameObject.SetActive(true);
        _text.text = string.Format(_template, value);
    }
}
