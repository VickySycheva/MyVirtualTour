using System;
using TMPro;
using UnityEngine;

public class PointButton : MonoBehaviour
{
    [SerializeField] private ApartmentPoint _pointToActivate;
    [SerializeField] private TMP_Text _text;

    private Action _onClickAction;

    public void Init(Action onClickAction)
    {
        _onClickAction = onClickAction;

        _text.text = _pointToActivate.Name;
    }

    void OnMouseDown()
    {
        _onClickAction.Invoke();
        _pointToActivate.Activate();
    }
}
