using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _contentItems;

    [SerializeField] private ShopCategoryButton _upgradesItem;
    [SerializeField] private ShopCategoryButton _itemsButton;

    [SerializeField] private ShopPanel _shopPanel;

    private IDataProvider _dataProvider;

    private ShopItemtView _previewedItem;

    private IEnumerable<ShopItem> _currentShopItems;

    private Wallet _wallet;
    private Income _income;
    private ClickableItem _clickable;

    private Level _level;

    private CountItemChecker _countItemChecker;
    private CountItemVisitor _countItemVisitor;

    private ChangeItemVisitor _changeItemVisitor;
    private CurrentItemIDChecker _currentItemIDChecker;
    private UnlockerItemVisitor _unlockerItemVisitor;
    private OpenItemChecker _openItemChecker;

    private void OnEnable()
    {
        _itemsButton.Click += OnItemButtonClick;
        _upgradesItem.Click += OnUpgradesItemButtonClick;
    }

    private void OnDisable()
    {
        _itemsButton.Click -= OnItemButtonClick;
        _upgradesItem.Click -= OnUpgradesItemButtonClick;

        _shopPanel.ItemViewClicked -= OnItemViewClicked;
        _level.LevelChanged -= ChangeLevel;
    }

    [Inject]
    public void Init(IDataProvider dataProvider, Wallet wallet, Income income, Level level, ClickableItem clickable,
    CountItemChecker countItemChecker, CountItemVisitor countItemVisitor, ChangeItemVisitor changeItemVisitor, CurrentItemIDChecker currentItemIDChecker, OpenItemChecker openItemChecker, UnlockerItemVisitor unlockerItemVisitor)
    {
        _wallet = wallet;

        _income = income;

        _clickable = clickable;

        _dataProvider = dataProvider;

        _level = level;

        _level.LevelChanged += ChangeLevel;

        _countItemChecker = countItemChecker;
        _countItemVisitor = countItemVisitor;

        _changeItemVisitor = changeItemVisitor;
        _currentItemIDChecker = currentItemIDChecker;
        _openItemChecker = openItemChecker;
        _unlockerItemVisitor = unlockerItemVisitor;

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        OnUpgradesItemButtonClick();
    }

    private void OnItemViewClicked(ShopItemtView item)
    {
        _previewedItem = item;

        if (!CanBuyItem(_previewedItem.Price))
            return;

        _openItemChecker.Visit(_previewedItem.Item);

        if (!_openItemChecker.IsOpened)
            _unlockerItemVisitor.Visit(_previewedItem.Item);
        else
        {
            _changeItemVisitor.Visit(_previewedItem.Item);
            _countItemVisitor.Visit(_previewedItem.Item);

        }


        _countItemChecker.Visit(_previewedItem.Item);
        _currentItemIDChecker.Visit(_previewedItem.Item);

        _previewedItem.SetCount(_countItemChecker.Count);
        _previewedItem.ChangeItem(_currentItemIDChecker.ID);

        _shopPanel.ChangeItems();

        if (_previewedItem.TypeItem == ItemType.AutoClick)
            _income.ChangeIncome();

        if (_previewedItem.TypeItem == ItemType.Clickable)
        {
            _clickable.ChangedItem();
            _income.ChangeIncome();
        }

        //_dataProvider.Save();
    }

    private void OnItemButtonClick()
    {
        _itemsButton.Select();
        _upgradesItem.UnSelect();

        _currentShopItems = _contentItems.ItemShopItems;

        _shopPanel.Show(_contentItems.ItemShopItems);
    }

    private void OnUpgradesItemButtonClick()
    {
        _itemsButton.UnSelect();
        _upgradesItem.Select();

        _currentShopItems = _contentItems.UpdatesShopItems;

        _shopPanel.Show(_contentItems.UpdatesShopItems);
    }

    private void ChangeLevel() =>
        _shopPanel.ChangeItems();

    private bool CanBuyItem(int price)
    {
        if (!_wallet.IsEnough(price))
            return false;

        _wallet.Spend(price);

        return true;
    }
}
