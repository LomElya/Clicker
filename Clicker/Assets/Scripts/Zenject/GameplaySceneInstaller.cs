using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [Header("Main Item")]
    [SerializeField] private ClickableItemView _prefabItem;
    [SerializeField] private Transform _parentItemSpawnPoint;
    [SerializeField] private ItemFactory _itemFactory;


    public override void InstallBindings()
    {
        BindClickable();

        BindClickableItem();

        BidnAnimation();
    }

    private void BindClickable() =>
        Container.Bind<ClickableItem>().AsSingle();

    private void BindClickableItem()
    {
        Container.Bind<ItemFactory>().FromInstance(_itemFactory);
        ClickableItemView item = Container.InstantiatePrefabForComponent<ClickableItemView>(_prefabItem, _parentItemSpawnPoint);
        Container.BindInterfacesAndSelfTo<ClickableItemView>().FromInstance(item).AsSingle();
    }

    private void BidnAnimation()
    {
        //StringValueView view = Container.InstantiatePrefabForComponent<StringValueView>(_textPrefab, _spawnPointAnimation);
        //Container.BindInterfacesAndSelfTo<ProfitClickDisplay>().AsSingle();

       // Container.Bind<ProfitClickAnimation>().AsSingle().NonLazy();
    }
}
