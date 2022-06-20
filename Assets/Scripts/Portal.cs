using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{

    public string[] scenes;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            GameManager.instance.SaveGame();

            string scene = scenes[Random.Range(0, scenes.Length)];
            SceneManager.LoadScene(scene); //change scene
        }
    }

}
