using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingMenu;
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void GameSetting()
    {
        _settingMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
