using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindData();

        BindWallet();

        BindVisitor();

        BindLevel();

        BindIncome();
    }

    private void BindData()
    {
        Container.BindInterfacesAndSelfTo<PersistentData>().AsSingle();
        Container.BindInterfacesAndSelfTo<DataLocalProvider>().AsSingle();
        Container.Bind<Bootstrap>().AsSingle().NonLazy();
    }

    private void BindWallet() =>
        Container.Bind<Wallet>().AsSingle();


    private void BindLevel() =>
        Container.Bind<Level>().AsSingle();

    private void BindIncome() =>
        Container.Bind<Income>().AsSingle();

    private void BindVisitor()
    {
        Container.BindInterfacesAndSelfTo<SelectedItemChecker>().AsSingle();

        Container.BindInterfacesAndSelfTo<CountItemChecker>().AsSingle();
        Container.BindInterfacesAndSelfTo<CountItemVisitor>().AsSingle();
        Container.BindInterfacesAndSelfTo<SelectItem>().AsSingle();

        Container.BindInterfacesAndSelfTo<CurrentLevelChecker>().AsSingle();
    }
}
