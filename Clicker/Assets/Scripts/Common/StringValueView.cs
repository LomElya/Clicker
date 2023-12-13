using UnityEngine;
using DG.Tweening;

public class StringValueView : ValueView<string>
{
    [SerializeField] private string _template;

    public override void Show(string value)
    {
        gameObject.SetActive(true);
        _text.text = string.Format(_template, value);

        _text.DOFade(100, 0);
    }

    public void Fade(float duraction) =>
        _text.DOFade(0, duraction);

}
