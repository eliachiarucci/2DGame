using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject menu;
    public static GameMenu instance;
    public GameObject[] windows;
    
    private CharStats[] playerStats;

    public Text[] nameText, HPText, MPText, lvlText, EXPText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

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
            for (int i = 0; i < windows.Length; i++)
            {
                windows[i].SetActive(false);
            }
            GameManager.instance.gameMenuOpen = false;
        }
        else
        {
            menu.SetActive(true);
            GameManager.instance.gameMenuOpen = true;
        }
    }

    public void closeMenu()
    {
        menu.SetActive(false);
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        GameManager.instance.gameMenuOpen = false;
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                HPText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                MPText[i].text = "Mana: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;
                lvlText[i].text = "Level: " + playerStats[i].playerLevel.ToString();
                EXPText[i].text = playerStats[i].getCurrentLevelExp()[0] + "/" + playerStats[i].getCurrentLevelExp()[1];
                expSlider[i].maxValue = playerStats[i].getCurrentLevelExp()[1];
                expSlider[i].value = playerStats[i].getCurrentLevelExp()[0];
                charImage[i].sprite = playerStats[i].charImage;
            } else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            } 
        }
    }

}
