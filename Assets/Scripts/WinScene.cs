using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Menu()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Main");
        GameManager.instance.player.Revive();
        GameManager.instance.ResetEverything();
        GameManager.instance.menuScreen.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
