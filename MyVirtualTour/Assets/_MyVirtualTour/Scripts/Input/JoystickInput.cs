using UnityEngine;

public class JoystickInput : IInput
{
    private Joystick _joystick;

    public JoystickInput(Joystick joystick) => _joystick = joystick;

    public Vector2 GetInputValues() => new Vector2(_joystick.Direction.x, _joystick.Direction.y * (-1));
} 
