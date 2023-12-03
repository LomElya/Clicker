using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class ClickableItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ClickableItemView> Click;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _multiplayerText;

    private Image _image;

    private ClickableItem _clickable;
    private ItemFactory _itemFactory;
    private ItemConfig _itemConfig;

    private int _id;

    private int _multiplier;

    private void OnDisable() =>
        _clickable.ItemChanged -= SetItem;


    [Inject]
    public void Init(ClickableItem clickable, ItemFactory itemFactory)
    {
        _image = GetComponent<Image>();

        _clickable = clickable;

        _itemFactory = itemFactory;

        _id = _clickable.GetCurrentItem();

        _clickable.ItemChanged += SetItem;
        SetItem(_id);
    }

    public int Multiplier
    {
        get => _multiplier;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _multiplier = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    private void SetItem(int id)
    {
        _id = id;

        _itemConfig = _itemFactory.Get(_id);

        _multiplier = _itemConfig.Multiplier;

        _nameText.text = _itemFactory.Name;
        _image.sprite = _itemFactory.Image;

        _multiplayerText.text = "Множитель: x" + _multiplier.ToString();
    }

}
