using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameIsEnd;
    [SerializeField] private GameObject _pauseWinMenu;
    [SerializeField] private GameObject _pauseLoseMenu;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerShoot;

    void Update()
    {
        if (_gameIsEnd.GetComponent<CheckBooks>().gameIsEnd)
        {
            EndGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }
        if (_player.GetComponent<Player>()._health == 0)
        {
            OpenLoseMenu();
        }
    }
    public void OpenSetting()
    {
        _settings.SetActive(true);   
    }
    public void OpenPauseMenu()
    {
        if (_pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            _playerShoot.GetComponent<PlayerShooter>()._gameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            _pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _playerShoot.GetComponent<PlayerShooter>()._gameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            _pauseMenu.SetActive(false);
        }
    }
    public void OpenLoseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _pauseLoseMenu.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _pauseWinMenu.SetActive(true);
    }
}
