using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuInputSelector : MonoBehaviour
{
    [SerializeField] private ToggleGroup _inputToggleGroup;

    [SerializeField] private Toggle _inputOptionJoystick;
    [SerializeField] private Toggle _inputOptionSwipes;

    #region Injected

    private InputService _inputService;

    #endregion

    [Inject]
    private void Construct(InputService inputService)
    {
        _inputService = inputService;
    }

    private void Start()
    {
        InputType savedInputType = (InputType)PlayerPrefs.GetInt("input_type", 0);

        SubscribeToggles();
        SetStartOption(savedInputType);
    }

    private void SubscribeToggles()
    {
        _inputOptionJoystick.onValueChanged.AddListener(SetInputJoystick);
        _inputOptionSwipes.onValueChanged.AddListener(SetInputSwipes);
    }

    private void SetStartOption(InputType savedInputOption)
    {
        switch (savedInputOption)
        {
            case InputType.Joystick:
                _inputOptionJoystick.SetIsOnWithoutNotify(true);
                break;
            case InputType.Swipes:
                _inputOptionSwipes.SetIsOnWithoutNotify(true);
                break;
        }

        SetInput(savedInputOption);
    }

    private void SetInputJoystick(bool isOn)
    {
        if (isOn == false) return;
        SetInput(InputType.Joystick);
    }

    private void SetInputSwipes(bool isOn)
    {
        if (isOn == false) return;
        SetInput(InputType.Swipes);
    }

    private void SetInput(InputType inputType)
    {
        _inputService.SetInputType(inputType);

        PlayerPrefs.SetInt("input_type", (int)inputType);
    }
}
