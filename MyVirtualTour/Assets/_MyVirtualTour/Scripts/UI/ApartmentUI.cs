using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ApartmentUI : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _exitButton;

    private SignalBus _signalBus;
    private InputService _inputService;

    [Inject]
    private void Construct(SignalBus signalBus, InputService inputService)
    {
        _signalBus = signalBus;
        _inputService = inputService;
        
        _inputService.SetJoystick(_joystick);
    }

    private void Start() 
    {
        _exitButton.onClick.AddListener(OnClickExitButton);    
    }

    private void OnClickExitButton()
    {
        _signalBus.Fire(new SignalOnExitApartment());
    }

    private void OnEnable()
    {
        _joystick.gameObject.SetActive(_inputService.GetInputType() == InputType.Joystick);
    }
}