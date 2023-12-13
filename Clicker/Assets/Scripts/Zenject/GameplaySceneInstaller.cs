using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private IncomeVisualizer _incomeVisualizer;

    [Header("Main Item")]
    [SerializeField] private ClickableItemView _prefabItem;
    [SerializeField] private Transform _parentItemSpawnPoint;
    [SerializeField] private ItemFactory _itemFactory;

    [Header("Shop")]
    [SerializeField] private Shop _prefabShop;
    [SerializeField] private Transform _parentShopSpawnPoint;
    [SerializeField] private ShopContent _contentShop;
    [SerializeField] private ShopItemViewFactory _shopObjectViewFactory;

    [Header("Other")]
    [SerializeField] private LevelDataConfig _levelDataConfig;


    public override void InstallBindings()
    {
        BindIncomeVisualizer();

        BindConfig();

        BindClickable();

        BindShop();

        BindClickableItem();
    }

    private void BindIncomeVisualizer() =>
         Container.Bind<IncomeVisualizer>().FromInstance(_incomeVisualizer).AsSingle().NonLazy();

    private void BindClickable() =>
        Container.Bind<ClickableItem>().AsSingle();

    private void BindShop()
    {
        Container.Bind<ShopContent>().FromInstance(_contentShop).AsSingle();
        Container.Bind<ShopItemViewFactory>().FromInstance(_shopObjectViewFactory).AsSingle();

        Shop shop = Container.InstantiatePrefabForComponent<Shop>(_prefabShop, _parentShopSpawnPoint);
        Container.BindInterfacesAndSelfTo<Shop>().FromInstance(shop).AsSingle();
    }

    private void BindClickableItem()
    {
        Container.Bind<ItemFactory>().FromInstance(_itemFactory).AsSingle();
        ClickableItemView item = Container.InstantiatePrefabForComponent<ClickableItemView>(_prefabItem, _parentItemSpawnPoint);
        Container.BindInterfacesAndSelfTo<ClickableItemView>().FromInstance(item).AsSingle();
    }

    private void BindConfig()
    {
        Container.Bind<LevelDataConfig>().FromInstance(_levelDataConfig).AsSingle();
    }
}
