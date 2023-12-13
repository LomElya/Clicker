using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : ButtonMain
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _unSelectColor;

    public void Select() => _image.color = _selectColor;
    public void UnSelect() => _image.color = _unSelectColor;
}
