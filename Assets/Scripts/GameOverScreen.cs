using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }


    private void Dissapear()
    {
        Disable();
        SceneManager.LoadScene("Main");
        GameManager.instance.player.Revive();
        GameManager.instance.ResetEverything();
    }

    public void Respawn()
    {
        Dissapear();
    }


    public void Menu()
    {
        Dissapear();
        GameManager.instance.menuScreen.Setup();
    }
}
