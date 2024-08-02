using System;
using UnityEngine;

public class ApartmentPoint : MonoBehaviour 
{
    [SerializeField] private string _pointName;
    [SerializeField] private Transform _cameraPosition;

    [SerializeField] private GameObject _apartmentRenderer;
    [SerializeField] private PointButton[] _pointButtons;

    public string Name => _pointName;

    private Action<Vector3> _onActive;

    public void Init(Action<Vector3> onActive)
    {
        _onActive = onActive;

        InitButtons();
    }

    private void InitButtons()
    {
        foreach (var button in _pointButtons)
        {
            button.Init(Deactivate);
        }
    }

    public void Activate()
    {
        _apartmentRenderer.SetActive(true);
        foreach (var button in _pointButtons)
        {
            button.gameObject.SetActive(true);
        }

        _onActive.Invoke(_cameraPosition.position);
    }

    public void Deactivate()
    {
        _apartmentRenderer.SetActive(false);
        foreach (var button in _pointButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
