using UnityEngine;
using Zenject;

public class ApartmentManager : MonoBehaviour
{
    [SerializeField] private ApartmentCamera _apartmentCamera;

    [SerializeField] private ApartmentPoint[] _apartmentPoints;
    [SerializeField] private ApartmentPoint _startPoint;

    private InputService _inputService;

    [Inject]
    private void Construct(InputService inputService)
    {
        _inputService = inputService;
    }

    private void Start()
    {
        _inputService.SetApartment(_apartmentCamera);

        foreach (var point in _apartmentPoints)
        {
            point.Init(MoveCamera);
            point.Deactivate();
        }

        _startPoint.Activate();
    }

    private void MoveCamera(Vector3 newPosition) => _apartmentCamera.transform.position = newPosition;
}
