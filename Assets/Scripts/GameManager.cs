using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //Different screens and scenes
    public static GameManager instance;
    public GameOverScreen gameOverScreen;
    public WinScene winScreen;
    public MenuScreen menuScreen;


    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;

    //Game logic
    public int maxHealth = 20;
    public int minhealth = 1;
    public int coins;
    public int xp;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    public void Start()
    {
        menuScreen.Setup();
        DontDestroyOnLoad(gameObject);
    }
    private void Awake()
    {
        
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(menuScreen.transform.parent.gameObject); //The HUD
            return;
        }


        instance = this;
        SceneManager.sceneLoaded += LoadGame;
        SceneManager.sceneLoaded += LoadOnScene;
    }
    
    //Try upgrading weapon
    public bool TryUpgradeWeapon()
    {
        Debug.Log(weaponPrices.Count + " price");
        Debug.Log(weapon.level + " level");
        if (weaponPrices.Count <= weapon.level - 1)
            return false;

        if(coins >= weaponPrices[weapon.level])
        {
            coins -= weaponPrices[weapon.level];
            weapon.UpgradeWeapon();
            ShowText("Upgraded Weapon!!", 25, Color.yellow, transform.position, Vector3.up * 30, 1f);
            return true;
        }

        return false;
    }

    //Check health to update HUD information
    public void CheckHealt()
    {
        Text healthText = GameObject.Find("Health").GetComponent<Text>();

        healthText.text = player.getHealth().ToString();

        if(player.getHealth() <= minhealth)
        {
            GameOver();
        }
    }

    //Try upgrading level
    public bool TryUpgradeLevel()
    {
        if (xpTable.Count <= player.level)
            return false;

        if (xp >= xpTable[player.level])
        {
            xp -= xpTable[player.level];
            player.LevelUp();
            ShowText("Level up!!", 25, Color.magenta, transform.position, Vector3.up * 30, 1f);
            return true;
        }

        return false;
    }

    //Show floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Make a load on scene
    public void LoadOnScene(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Save game status
    public void SaveGame()
    {
        
        string s = "";

        s += "0" + "|"; //current skin
        s += coins.ToString() + "|"; //num coins
        s += xp.ToString() + "|"; //amount xp
        s += "0"; //actual weapon

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Saved state");
    }

    //Load game status
    public void LoadGame(Scene s, LoadSceneMode mode)
    {

        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        this.coins = int.Parse(data[1]);
        this.xp = int.Parse(data[2]);


        Debug.Log("Loaded state");

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        GameManager.instance.CheckHealt();
        PlayerPrefs.DeleteAll();
    }

    //Reset the game
    internal void ResetEverything()
    {
        player.Heal(maxHealth, false);
        coins = 0;
        xp = 0;
        weapon.Reset();
    }

    //Execute GameOver
    private void GameOver()
    {
        player.Die();
        gameOverScreen.Setup();
    }

    //Execute Win
    private void Win()
    {
        winScreen.Setup();
    }
}
