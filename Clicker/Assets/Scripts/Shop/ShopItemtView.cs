using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class ShopItemtView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemtView> Click;
    [Header("Sprites")]
    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;

    [Header("Images")]
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [Header("Text")]
    [SerializeField] private IntValueView _priceView;
    [SerializeField] private StringValueView _countView;

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;

    private Image _backgroundImage;
    private ShopItemConfig _config;
    private int _id;

    public bool IsLock { get; private set; }
    public ShopItem Item { get; private set; }

    public string Name => Item.Name;
    public ItemType TypeItem => Item.TypeItem;
    public String Description => Item.Description;
    public int MaxCount => Item.MaxCount;

    public int Price => _config.Price;
    public Sprite Image => _config.Image;
    public int OpenAtLevel => _config.OpenAtLevel;

    public bool IsMaxCount => _id == MaxCount;

    public void Init(ShopItem item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;


        Item = item;
        ChangeInfo();
    }

    public void SetCount(int id)
    {
        _id = id;
        ChangeInfo();
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Hide();
        _countView.Hide();
    }

    public void UnLock()
    {
        IsLock = false;

        if (IsMaxCount)
        {
            MaxItem();
            return;
        }

        _lockImage.gameObject.SetActive(IsLock);
        _countView.Show(_id.ToString());
        _priceView.Show(Price);
    }

    public void MaxItem()
    {
        _priceView.Hide();
        _countView.Show("MAX");
    }

    private void ChangeInfo()
    {
        if (IsMaxCount)
        {
            MaxItem();
            return;
        }

        _config = Item.Get(_id);
        _contentImage.sprite = Image;
        _nameText.text = Name;
        _descriptionText.text = Description;
        _countView.Show(_id.ToString());
        _priceView.Show(Price);

    }
}
