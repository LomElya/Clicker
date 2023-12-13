using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemtView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemtView> Click;

    [Header("Images")]
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [Header("Text")]
    [SerializeField] private PriceValueView _priceView;
    [SerializeField] private StringValueView _countView;
    [SerializeField] private StringValueView _lvlView;
    [SerializeField] private StringValueView _descriptionText;
    [SerializeField] private StringValueView _openAtLevelText;

    [SerializeField] private TMP_Text _nameText;

    private ShopItemConfig _config;
    
    private int _id;
    private int _count;

    public bool IsLock { get; private set; }
    public bool IsShow { get; private set; }
    public ShopItem Item { get; private set; }

    public string Name => Item.Name;
    public ItemType TypeItem => Item.TypeItem;
    public String Description => Item.Description;
    public int AddCount => Item.AddCount;
    public int MaxCount => Item.MaxCount;

    public int Price => _config.Price;
    public Sprite Image => _config.Image;
    public int OpenAtLevel => _config.OpenAtLevel;

    public bool IsMaxCount => _id == MaxCount;

    public void Init(ShopItem item)
    {
        Item = item;

        ChangeInfo();
    }

    public void ChangeItem(int id)
    {
        _id = id + 1;

        ChangeInfo();
    }

    public void SetCount(int count)
    {
        _count = count;

        _countView.Show(_count.ToString());
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Show(bool isShop)
    {
        IsShow = isShop;
        gameObject.SetActive(IsShow);
    }

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);

        _priceView.Hide();
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

        //_countView.Show(_count.ToString());

        //_lvlView.Show(_id.ToString());

        _priceView.Show(Price);

    }

    public void MaxItem()
    {
        _priceView.Hide();

        _lvlView.Show("MAX");
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

        _descriptionText.Show(AddCount + " " + Description);

        _lvlView.Show(_id.ToString());
        _priceView.Show(Price);
        _openAtLevelText.Show(OpenAtLevel.ToString());

    }
}
