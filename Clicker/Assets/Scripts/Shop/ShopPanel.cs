using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemtView> ItemViewClicked;

    private List<ShopItemtView> _shopItems = new List<ShopItemtView>();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopObjectViewFactory;

    private Level _level;

    private CountItemChecker _countItemChecker;

    [Inject]
    public void Init(Level level,
        CountItemChecker countItemChecker)
    {
        _level = level;

        _countItemChecker = countItemChecker;
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();

        foreach (ShopItem item in items)
        {
            ShopItemtView spawnedItem = _shopObjectViewFactory.Get(item, _itemsParent);

            _countItemChecker.Visit(spawnedItem.Item);

            spawnedItem.SetCount(_countItemChecker.Count);

            spawnedItem.Click += OnItemViewClick;

            if (_level.GetCurrentLevel() >= spawnedItem.OpenAtLevel)
            {
                spawnedItem.UnLock();
            }
            else
            {
                spawnedItem.Lock();
            }

            _shopItems.Add(spawnedItem);
        }
    }

    public void Select(ShopItemtView itemView)
    {
        //Для скрытия кнопки "куплено/Boughh" всех кроме выбранно
        /*   foreach (var item in _shopItems)
              item.UnSelect(); */

        //itemView.Select();
    }

    public void ChangeItems()
    {
        foreach (var item in _shopItems)
        {
            if (_level.GetCurrentLevel() >= item.OpenAtLevel || item.IsMaxCount)
                item.UnLock();
            else
                item.Lock();
        }
    }

    private void OnItemViewClick(ShopItemtView item)
    {
        Highlight(item);

        if (item.IsLock || item.IsMaxCount)
            return;

        ItemViewClicked?.Invoke(item);
    }

    private void Highlight(ShopItemtView shopObjectView)
    {
        /*  foreach (var item in _shopItems)
             item.UnHighlight();

         shopObjectView.Highlight(); */
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
