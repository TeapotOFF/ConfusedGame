using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private AudioSource _shooterClip;
    [SerializeField] private GameObject _cameraContainer;
    [SerializeField] private GameObject _hitBloodEffect;
    [SerializeField] private GameObject _hitFireEffect;
    [SerializeField] private ParticleSystem _pistolFire;

    public float _ammoInStore = 7;
    public float _allAmmo = 16;
    public bool _gameIsPaused = false;

    void Update()
    {
        Fire();
    }
    private void Reload()
    {
        if (_ammoInStore < 7 && !(_allAmmo <= 0))
        {
            if ((7 - _ammoInStore) > _allAmmo)
            {
                _ammoInStore += _allAmmo;
                _allAmmo -= _allAmmo;
            }
            else
            {
                _allAmmo -= 7 - _ammoInStore;
                _ammoInStore += 7 - _ammoInStore;
            }
        }
    }
    private void Fire()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        transform.localEulerAngles = new Vector3(_cameraContainer.transform.eulerAngles.x, 0f, 0f);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
        if (Input.GetKeyDown(KeyCode.Mouse0) && (_ammoInStore != 0) && !_gameIsPaused)
        {
            _pistolFire.Play();
            _shooterClip.Play();
            _ammoInStore -= 1;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20f))
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    _hitBloodEffect.transform.position = hit.point;
                    _hitBloodEffect.GetComponent<ParticleSystem>().Play();
                    hit.collider.gameObject.GetComponent<EnemyLogic>().TakeDamege();

                }
                else
                {
                    _hitFireEffect.transform.position = hit.point;
                    _hitFireEffect.GetComponent<ParticleSystem>().Play();
                }
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
    }

}
