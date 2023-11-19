using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ClickableItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ClickableItemView> Click;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _multiplayerText;

    private ItemFactory _itemFactory;
    private ItemConfig _itemConfig;

    private int _id;

    private int _multiplier;

    SelectedItemChecker _selectedItemChecher;

    public int ID => _id;

    [Inject]
    public void Init(ItemFactory itemFactory, SelectedItemChecker selectedItemChecher)
    {
        _itemFactory = itemFactory;
        _selectedItemChecher = selectedItemChecher;

        _selectedItemChecher.Visit();

        _id = _selectedItemChecher.ID;

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

    public void SetItem(int id)
    {
        _itemConfig = _itemFactory.Get(id);

        _image.sprite = _itemConfig.Image;
        _multiplier = _itemConfig.Multiplier;
        _nameText.text = _itemConfig.Name;
        _multiplayerText.text = "Множитель: x" + _multiplier.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
}
