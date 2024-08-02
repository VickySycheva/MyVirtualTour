using UnityEngine;

public class SwipeInput : IInput
{
    public Vector2 GetInputValues()
    {
        if(Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            return new Vector2(touch.deltaPosition.x * (-1), touch.deltaPosition.y);
        }
        return Vector2.zero;
    }
}
