using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemtView> ItemViewClicked;

    private List<ShopItemtView> _shopItems = new List<ShopItemtView>();

    [SerializeField] private Transform _itemsParent;
    private ShopItemViewFactory _shopObjectViewFactory;

    private Level _level;

    private CountItemChecker _countItemChecker;
    private CurrentItemIDChecker _currentItemIDChecker;
    private OpenItemChecker _openItemChecker;

    [Inject]
    public void Init(Level level, ShopItemViewFactory shopItemViewFactory,
        CountItemChecker countItemChecker, CurrentItemIDChecker currentItemIDChecker, OpenItemChecker openItemChecker)
    {
        _level = level;
        _shopObjectViewFactory = shopItemViewFactory;

        _countItemChecker = countItemChecker;
        _currentItemIDChecker = currentItemIDChecker;
        _openItemChecker = openItemChecker;
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();

        foreach (ShopItem item in items)
        {
            ShopItemtView spawnedItem = _shopObjectViewFactory.Get(item, _itemsParent);

            _openItemChecker.Visit(spawnedItem.Item);

            _countItemChecker.Visit(spawnedItem.Item);
            _currentItemIDChecker.Visit(spawnedItem.Item);

            spawnedItem.SetCount(_countItemChecker.Count);

            spawnedItem.ChangeItem(_currentItemIDChecker.ID);

            LockedItem(spawnedItem);

            spawnedItem.Click += OnItemViewClick;

            _shopItems.Add(spawnedItem);
        }
    }

    public void ChangeItems()
    {
        foreach (var item in _shopItems)
            LockedItem(item);
    }

    private void OnItemViewClick(ShopItemtView item)
    {
        if (item.IsLock || item.IsMaxCount)
            return;

        ItemViewClicked?.Invoke(item);
    }

    private void LockedItem(ShopItemtView item)
    {
        if (item.IsMaxCount || _level.GetCurrentLevel() >= item.OpenAtLevel)
            item.UnLock();
        else
            item.Lock();
    }

    private void Clear()
    {
        foreach (ShopItemtView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
