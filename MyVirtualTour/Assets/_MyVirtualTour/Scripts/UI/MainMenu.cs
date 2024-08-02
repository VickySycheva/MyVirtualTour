using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuInputSelector _mainMenuInputSelector;

    [SerializeField] private GameObject _mainMenuContent;
    [SerializeField] private GameObject _chooseApartmentContent;

    [SerializeField] private GameObject _apartmentButtonsContainer;
 
    [SerializeField] private Button _openApartmentListButton;
    [SerializeField] private Button _runSceneButton;
    [SerializeField] private Button _closeApartmentListButton;

    private ApartmentsListSO _apartmentsListSO;
    private SignalBus _signalBus;
    private ApartmentButton.Factory _apartmentButtonFactory;
    private List<ApartmentButton> _apartmentButtons;
    private ApartmentButton _selectedApartmentButton;

    [Inject]
    private void Construct(ApartmentsListSO apartmentsListSO,
                           SignalBus signalBus,
                           ApartmentButton.Factory apartmentButtonFactory)
    {
        _apartmentsListSO = apartmentsListSO;
        _signalBus = signalBus;
        _apartmentButtonFactory = apartmentButtonFactory;
    }

    private void Start()
    {
        _runSceneButton.onClick.AddListener(LoadLevel);
        _openApartmentListButton.onClick.AddListener(OpenApartmentList);
        _closeApartmentListButton.onClick.AddListener(CloseApartmentList);

        _apartmentButtons = new List<ApartmentButton>();
        foreach (ApartmentData data in _apartmentsListSO.Apartments)
        {
            ApartmentButton button = _apartmentButtonFactory.Create();

            button.transform.SetParent(_apartmentButtonsContainer.transform);
            button.transform.localScale = Vector3.one;
            button.transform.localPosition = Vector3.zero;

            button.Init(data, OnAppartmentButtonClick);
            button.Deselect();

            _apartmentButtons.Add(button);
        }
    }

    public void LoadMainMenu()
    {
        CloseApartmentList();
    }

    private void OpenApartmentList()
    {
        _mainMenuContent.gameObject.SetActive(false);
        _chooseApartmentContent.gameObject.SetActive(true);

        OnAppartmentButtonClick(_apartmentButtons[0]);
    }

    private void CloseApartmentList()
    {
        _mainMenuContent.gameObject.SetActive(true);
        _chooseApartmentContent.gameObject.SetActive(false);
    }

    private void OnAppartmentButtonClick(ApartmentButton button)
    {
        if (_selectedApartmentButton != null && _selectedApartmentButton == button) return;
        if (_selectedApartmentButton != null) _selectedApartmentButton.Deselect();

        _selectedApartmentButton = button;
        _selectedApartmentButton.Select();
    }

    private void LoadLevel()
    {
        _signalBus.Fire(new SignalLoadScene { SceneToLoad = _selectedApartmentButton.ApartmentData.ApartmentScene });
    }
    
}
