using System;
using UniRx;
using UnityEngine;

public class InputService
{
    private InputType _inputType;

    private IInput _input;

    private ApartmentCamera _apartmentCamera;
    private Joystick _joystick;

    private IDisposable _inputUpdate;    

    public InputType GetInputType() => _inputType;

    public void SetInputType(InputType inputType) => _inputType = inputType;
    public void SetJoystick(Joystick joystick) => _joystick = joystick;

    public void SetApartment(ApartmentCamera apartmentCamera)
    {
        _apartmentCamera = apartmentCamera;
        InitInput();

        _inputUpdate = Observable.EveryUpdate().Subscribe(_ => MoveCamera());
    }

    public void RemoveApartment()
    {
        _inputUpdate.Dispose();
    }

    private void MoveCamera()
    {
        Vector2 rotation = _input.GetInputValues();

        Vector3 horizontalAngle = _apartmentCamera.TransformForHorizontalRotation.localEulerAngles;
        Vector3 verticalAngle = _apartmentCamera.TransformForVerticalRotation.localEulerAngles;

        horizontalAngle +=  Vector3.up * rotation.x * 0.1f;
        verticalAngle += Vector3.right * rotation.y * 0.1f;

        _apartmentCamera.TransformForHorizontalRotation.localEulerAngles = horizontalAngle;
        _apartmentCamera.TransformForVerticalRotation.localEulerAngles = verticalAngle;
    }

    private void InitInput()
    {
        switch (_inputType)
        {
            case InputType.Swipes:
                _input = new SwipeInput();
                break;
            case InputType.Joystick:
                _input = new JoystickInput(_joystick);
                break;
        }
    }


}
