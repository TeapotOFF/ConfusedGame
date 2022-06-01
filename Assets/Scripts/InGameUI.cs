using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _hpText;
    [SerializeField] private GameObject _ammoText;
    [SerializeField] private GameObject _playerRayCast;

    void Update()
    {
        UpdateAmmo();
        UpdateHP();
    }
    private void UpdateAmmo()
    {
        _ammoText.GetComponent<Text>().text = $"{_playerRayCast.GetComponent<PlayerShooter>()._ammoInStore} | {_playerRayCast.GetComponent<PlayerShooter>()._allAmmo}";
    }
    private void UpdateHP()
    {
        _hpText.GetComponent<Text>().text = $"HP : {_player.GetComponent<Player>()._health}";
    }
}
