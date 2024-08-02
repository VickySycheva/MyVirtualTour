using UnityEngine;

public class MainInstaller : BaseInstaller
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private ApartmentUI _apartmentUI;

    [SerializeField] private GameObject _apartmentButton;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainMenu);
        Container.BindInstance(_fadeScreen);
        Container.BindInstance(_apartmentUI);

        BindPrefabFactory<ApartmentButton, ApartmentButton.Factory>(_apartmentButton);
    }
}
