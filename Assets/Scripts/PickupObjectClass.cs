using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectClass : MonoBehaviour
{
    [SerializeField] private GameObject _pickupPoint;
    [SerializeField] private GameObject _spineTransform;
    private GameObject _currentObject;
    private Rigidbody _currentObjectRB;
    private float rigOffset = 0.5f;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 3f, Color.yellow);
        _pickupPoint.transform.position = transform.position + transform.forward * 3f;
        _spineTransform.transform.position = new Vector3(_pickupPoint.transform.position.x, _pickupPoint.transform.position.y - rigOffset, _pickupPoint.transform.position.z); ;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentObject == null)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3f))
                {
                    PickupObject(hit.collider.gameObject);
                }
            }
            else
            {
                BreakPickup();
            }
        }
        if (_currentObject != null)
        {
            Move();
        }
    }
    void Move()
    {
            if (Vector3.Distance(_currentObject.transform.position, _pickupPoint.transform.position) > 0.1f)
            {
                Vector3 direction = _pickupPoint.transform.position - _currentObject.GetComponent<Rigidbody>().transform.position;
                float DistanseToPoint = direction.magnitude;
                //_currentObject.GetComponent<Rigidbody>().AddForce(direction * _pickupForce);
                _currentObject.GetComponent<Rigidbody>().velocity = direction * DistanseToPoint * 12f;
            }
    }
    void PickupObject(GameObject _lookObject)
    {
        if (_lookObject.tag == "Pickup")
        {
            _currentObjectRB = _lookObject.GetComponent<Rigidbody>();
            _currentObjectRB.useGravity = false;
            _currentObjectRB.drag = 20;
            _currentObjectRB.constraints = RigidbodyConstraints.FreezeRotation;

            //_currentObjectRB.transform.parent = _pickupPoint.transform;
            _currentObject = _lookObject;
        }
    }
    void BreakPickup()
    {
        _currentObjectRB.useGravity = true;
        _currentObjectRB.drag = 0;
        _currentObjectRB.constraints = RigidbodyConstraints.None;

        //_currentObjectRB.transform.parent = null;
        _currentObject = null;
    }
}
