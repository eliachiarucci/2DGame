using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public static GameMenu instance;
    private CharStats[] playerStats;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Action()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
            GameManager.instance.gameMenuOpen = false;
        }
        else
        {
            menu.SetActive(true);
            GameManager.instance.gameMenuOpen = true;
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
    }

}
