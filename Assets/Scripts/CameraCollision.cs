using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private GameObject _cameraContainer;
    [SerializeField] private GameObject _target;
    private OrbitCamera _camera;
    private float minDistance = 1f;
    private float maxDistance = 6.8f;
    private float distance;
    void Awake()
    {
        _camera = _cameraContainer.GetComponent<OrbitCamera>();
    }
    void Update()
    {
        RaycastHit hit;
        Debug.DrawLine(_target.transform.position, transform.position, Color.white);
        if (Physics.Linecast(_target.transform.position, _cameraContainer.transform.position, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.9f),minDistance,maxDistance);  
        }
        else
        {
            distance = _camera._distanseToTarget;
        }
        transform.position = _target.transform.position - transform.forward * distance;
    }
}
