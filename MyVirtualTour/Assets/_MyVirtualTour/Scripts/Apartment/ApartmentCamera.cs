using UnityEngine;

public class ApartmentCamera : MonoBehaviour 
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pivot;
    
    public Transform TransformForVerticalRotation => _camera.transform;
    public Transform TransformForHorizontalRotation => _pivot;

    public void MoveCamera(Vector3 position) => _pivot.position = position;
}
