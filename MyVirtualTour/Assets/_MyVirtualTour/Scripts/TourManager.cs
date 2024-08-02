using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class TourManager : MonoBehaviour
{
    private SignalBus _signalBus;

    private MainMenu _mainMenu;
    private ApartmentUI _apartmentUI;
    private InputService _inputService;
    private FadeScreen _fadeScreen;

    private AssetReference _sceneToLoad;
    private AsyncOperationHandle<SceneInstance> ApartmentScene;

    [Inject]
    private void Construct(SignalBus signalBus,
                           MainMenu mainMenu,
                           FadeScreen fadeScreen,
                           ApartmentUI apartmentUI,
                           InputService inputService)
    {
        _signalBus = signalBus;
        _mainMenu = mainMenu;
        _fadeScreen = fadeScreen;
        _apartmentUI = apartmentUI;
        _inputService = inputService;

        _apartmentUI.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);
        _fadeScreen.gameObject.SetActive(true);

        _fadeScreen.DoFadeOut();

        _signalBus.Subscribe<SignalLoadScene>(LoadApartmentScene);
        _signalBus.Subscribe<SignalOnExitApartment>(ExitApartment);
    }

    #region Load Apartment
    private void LoadApartmentScene(SignalLoadScene arg)
    {
        _sceneToLoad = arg.SceneToLoad;
        _fadeScreen.DoFadeIn(onEnd: LoadApartmentSceneAfterFadeIn);
    }

    private void LoadApartmentSceneAfterFadeIn()
    {
        ApartmentScene = Addressables.LoadSceneAsync(_sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        ApartmentScene.Completed += LoadApartmentSceneComplete;
    }

    private void LoadApartmentSceneComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        ApartmentScene.Completed -= LoadApartmentSceneComplete;
        _sceneToLoad = null;

        _mainMenu.gameObject.SetActive(false);
        _apartmentUI.gameObject.SetActive(true);

        _fadeScreen.DoFadeOut();
    }
    #endregion

    #region Exit Apartment
    private void ExitApartment()
    {
        _inputService.RemoveApartment();
        _fadeScreen.DoFadeIn(onEnd: () => ExitApartmentAfterFadeIn());
    }

    private async UniTaskVoid ExitApartmentAfterFadeIn()
    {
        _apartmentUI.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(true);

        await Addressables.UnloadSceneAsync(ApartmentScene);

        _mainMenu.LoadMainMenu();
        _fadeScreen.DoFadeOut();
    }
    #endregion
}