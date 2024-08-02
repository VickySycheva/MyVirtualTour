using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ApartmentButton : MonoBehaviour
{
    public class Factory : PlaceholderFactory<ApartmentButton> {}

    [SerializeField] private TMP_Text _apartmentName;

    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _selectColor;

    public ApartmentData ApartmentData { get; private set; }
    private Action _onSelect;

    public void Init(ApartmentData apartmentData, Action<ApartmentButton> onSelect)
    {
        ApartmentData = apartmentData;

        _apartmentName.text = ApartmentData.ApartmentName;

        _onSelect += () => onSelect.Invoke(this);

        _button.onClick.AddListener(() => _onSelect.Invoke());
    }

    public void Select()
    {
        _buttonImage.color = _selectColor;
    }

    public void Deselect()
    {
        _buttonImage.color = Color.white;
    }
}
