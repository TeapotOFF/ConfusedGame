using System.Collections;
using UnityEngine;
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float _distanseToTarget = 6.8f;
    private float _rotationX;
    private float _rotationY;
    private float sens = 1.5f;
    public float rotSpeed = 1.5f;
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * sens;
        float yRotation = Input.GetAxis("Mouse Y") * sens;
        
        _rotationY += xRotation;
        _rotationX += yRotation;

        _rotationX = Mathf.Clamp(_rotationX, -40, 60);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, 0.1f);
        transform.localEulerAngles = _currentRotation;
        transform.position = player.position - transform.forward * _distanseToTarget;

        Aiming();
    }
    private void Aiming()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _distanseToTarget = Mathf.Lerp(_distanseToTarget, 3.8f, 0.01f);
        }
        else 
        {
            if (_distanseToTarget != 6.8f)
            {
                _distanseToTarget = Mathf.Lerp(_distanseToTarget, 6.8f, 0.01f);
            }
        }
    }
}
