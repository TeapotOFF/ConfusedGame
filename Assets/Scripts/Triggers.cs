using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    [SerializeField] private GameObject _playerRayCast;
    [SerializeField] private GameObject _altarText;
    public bool isAmmo;
    public bool isAltarText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isAmmo)
            {
                _playerRayCast.GetComponent<PlayerShooter>()._allAmmo += 14;
                Destroy(gameObject);
            }
            else if (isAltarText)
            {
                _altarText.gameObject.SetActive(true);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isAltarText)
            {
                _altarText.gameObject.SetActive(false);
            }

        }
    }
}
