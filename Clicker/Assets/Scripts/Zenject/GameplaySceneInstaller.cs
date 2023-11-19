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
        BindClickableItem();
    }

    private void BindClickableItem()
    {
        Container.Bind<ItemFactory>().FromInstance(_itemFactory);
        ClickableItemView item = Container.InstantiatePrefabForComponent<ClickableItemView>(_prefabItem, _parentItemSpawnPoint);
        Container.BindInterfacesAndSelfTo<ClickableItemView>().FromInstance(item).AsSingle();
    }
}
