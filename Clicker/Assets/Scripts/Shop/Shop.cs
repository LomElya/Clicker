using System.Linq;
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

    private Wallet _wallet;
    private Income _income;

    private Level _level;

    private CountItemChecker _countItemChecker;
    private CountItemVisitor _countItemVisitor;

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
    public void Init(IDataProvider dataProvider, Wallet wallet, Income income, Level level,
    CountItemChecker countItemChecker, CountItemVisitor countItemVisitor)
    {
        _wallet = wallet;

        _income = income;

        _dataProvider = dataProvider;

        _level = level;

        _level.LevelChanged += ChangeLevel;

        _countItemChecker = countItemChecker;
        _countItemVisitor = countItemVisitor;

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        OnUpgradesItemButtonClick();
    }

    private void OnItemViewClicked(ShopItemtView item)
    {
        _previewedItem = item;

        if (!BuyItem(_previewedItem.Price))
            return;

        _countItemVisitor.Visit(_previewedItem.Item);
        _countItemChecker.Visit(_previewedItem.Item);

        _previewedItem.SetCount(_countItemVisitor.Count);

        _shopPanel.ChangeItems();

        if (_previewedItem.TypeItem == ItemType.AutoClick)
            _income.ChangeIncome();
    }

    private void OnItemButtonClick()
    {
        _itemsButton.Select();
        _upgradesItem.UnSelect();
        //_shopPanel.Show(_contentItems.ImmovablesItemObjects.Cast<ShopObject>()); 

        _shopPanel.Show(_contentItems.ItemShopItems);
    }

    private void OnUpgradesItemButtonClick()
    {
        _itemsButton.UnSelect();
        _upgradesItem.Select();
        //_shopPanel.Show(_contentItems.IndustryItemObjects.Cast<ShopObject>());

        _shopPanel.Show(_contentItems.UpdatesShopItems);
    }

    private void ChangeLevel()
    {
        _shopPanel.ChangeItems();
    }

    private void SelectItem()
    {
        // _objectSelector.Visit(_previewedItem.Item);
        _shopPanel.Select(_previewedItem);
    }

    private bool BuyItem(int price)
    {
        if (!_wallet.IsEnough(price))
            return false;

        _wallet.Spend(price);

        return true;
    }
}
