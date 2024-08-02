using UnityEngine;
using Zenject;

public class ProjectInstaller : BaseInstaller
{
    [SerializeField] private ApartmentsListSO _apartmentsListSO;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        DeclareSignals();

        Container.BindInstance(_apartmentsListSO).AsSingle();

        Container.Bind<InputService>().AsSingle();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<SignalOnFadeInStart>().OptionalSubscriber();
        Container.DeclareSignal<SignalOnFadeInEnd>().OptionalSubscriber();
        Container.DeclareSignal<SignalOnFadeOutStart>().OptionalSubscriber();
        Container.DeclareSignal<SignalOnFadeOutEnd>().OptionalSubscriber();

        Container.DeclareSignal<SignalOnExitApartment>().OptionalSubscriber();

        Container.DeclareSignal<SignalLoadScene>();
    }
}
