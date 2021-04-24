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

    public Text statusPointsAvailable, statusCriticalChance, statusName, statusMP, statusBlock, statusDodge, statusAgility, statusStr, statusStamina, statusPhysicalDef, statusMagicalDef, statusMagicalDamage, statusWeaponDamage, statusFirstHandEquipped, statusSecondHandEquipped, statusArmorEquipped, statusExp, statusLevel;
    public Image statusImage;

    public ItemButton[] itemButtons;

    public Item activeItem;
    public Text itemName, itemDescription, itemPrice, useButtonText, stat1, stat2, stat3, stat4, stat5, stat6, stat7;

    public GameObject itemCharacterChoiceMenu;
    public Text[] itemCharacterChoiceNames;

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
        itemCharacterChoiceMenu.SetActive(false);
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
                if (windowNumber == 1)
                {
                    RefreshPlayerStatsUI();   
                }
            } else
            {
                windows[i].SetActive(false);
            } 
        }
        itemCharacterChoiceMenu.SetActive(false);
    }

    public void RefreshPlayerStatsUI()
    {
        statusName.text = playerStats[0].charName;
        statusPointsAvailable.text = "Points Available: " + playerStats[0].statsPointsAvailable.ToString();
        statusLevel.text = "Level: " + playerStats[0].playerLevel.ToString();
        statusExp.text = playerStats[0].getCurrentLevelExp()[0] + "/" + playerStats[0].getCurrentLevelExp()[1];
        statusWeaponDamage.text = playerStats[0].weaponDamage.ToString();
        statusPhysicalDef.text = playerStats[0].physicalDefense.ToString();
        statusMagicalDef.text = playerStats[0].magicalDefense.ToString();
        statusMP.text = playerStats[0].magicPower.ToString();
        statusAgility.text = playerStats[0].agility.ToString();
        statusStr.text = playerStats[0].strength.ToString();
        statusStamina.text = playerStats[0].stamina.ToString();
        statusCriticalChance.text = playerStats[0].critChance.ToString();
        statusBlock.text = playerStats[0].block.ToString();
        statusDodge.text = playerStats[0].dodge.ToString();
        statusFirstHandEquipped.text = playerStats[0].equippedFirstHand;
        statusSecondHandEquipped.text = playerStats[0].equippedSecondHand;
        statusArmorEquipped.text = playerStats[0].equippedArmor;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            GameManager gameManager = GameManager.instance;
            if(gameManager.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = gameManager.getItemDetails(gameManager.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = gameManager.numberOfItems[i].ToString();
                Item item = gameManager.getItemDetails(gameManager.itemsHeld[i]);
                CheckIfItemIsStillEquipped(item);
                if (item.equipped)
                {
                    itemButtons[i].outline.enabled = true;
                } else
                {
                    itemButtons[i].outline.enabled = false;
                }
            } else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    private void CheckIfItemIsStillEquipped(Item item)
    {
        List<Item> equippedArray = new List<Item>() { GameManager.instance.getItemDetails(playerStats[0].equippedFirstHand), GameManager.instance.getItemDetails(playerStats[0].equippedSecondHand), GameManager.instance.getItemDetails(playerStats[0].equippedArmor)};
        if (!equippedArray.Contains(item))
        {
            item.equipped = false;
        } else
        {
            item.equipped = true;
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        
        switch(activeItem.itemType)
        {
            case Item.ItemType.Potion: useButtonText.text = "Use"; break;
            case Item.ItemType.QuestItem: useButtonText.text = "Use"; break;
            case Item.ItemType.Armor: useButtonText.text = activeItem.equipped ? "Unequip" : "Equip"; break;
            case Item.ItemType.Weapon: useButtonText.text = activeItem.equipped ? "Unequip" : "Equip"; break;
            case Item.ItemType.Generic: useButtonText.text = "Use"; break;
            case Item.ItemType.Scroll: useButtonText.text = "Use"; break;
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
        itemPrice.text = activeItem.price.ToString() + " Gold";
        Text[] statsArray = { stat1, stat2, stat3, stat4, stat5, stat6, stat7 };
        Dictionary<string, int> statsObject = new Dictionary<string, int>() {
            {"Weapon Damage", activeItem.weaponDamage },
            {"HP", activeItem.modifyHP },
            {"Mana", activeItem.modifyMana },
            {"Magic Power", activeItem.modifyMP },
            {"Strength", activeItem.modifyStrength },
            {"Stamina", activeItem.modifyStamina },
            {"Agility", activeItem.modifyAgility },
            {"Dodge", activeItem.modifyDodge },
            {"Block", activeItem.modifyBlock },
            {"Physical Def", activeItem.modifyPhysicalDef },
            {"Magic Def", activeItem.modifyMagicDef }
        };



        for (int x = 0; x < statsArray.Length; x++)
        {
            statsArray[x].text = "";
        }

        if(activeItem.itemType == Item.ItemType.Armor || activeItem.itemType == Item.ItemType.Weapon || activeItem.itemType == Item.ItemType.Weapon2)
        {
            foreach (KeyValuePair<string, int> entry in statsObject)
            {
                if (statsObject[entry.Key] != 0)
                {
                    for (int x = 0; x < statsArray.Length; x++)
                    {
                        if (statsArray[x].text == "")
                        {
                            statsArray[x].text = entry.Key + ": " + entry.Value;
                            break;
                        }
                    }
                }
            }
        }
        
        itemCharacterChoiceMenu.SetActive(false);

    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void HandleUseItem()
    {
        switch(useButtonText.text)
        {
            case "Use": UseItem(0); break;
            case "Cast": OpenItemCharacterChoice(); break;
            case "Equip": GameManager.instance.EquipItem(activeItem.itemName); break;
            case "Unequip": GameManager.instance.UnEquip(activeItem.itemName); break;
        }
        ShowItems();
        SelectItem(activeItem);
    }

    public void OpenItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(true);
        for(int i = 0; i < itemCharacterChoiceNames.Length; i++)
        {
            itemCharacterChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharacterChoice();
    }

    public void assignPoint(int abilityPointType)
    {
        if(playerStats[0].statsPointsAvailable > 0) { 
            switch (abilityPointType)
            {
                case 0: playerStats[0].strengthAbilityPoints++; playerStats[0].statsPointsAvailable--; break;
                case 1: playerStats[0].staminaAbilityPoints++; playerStats[0].statsPointsAvailable--; break;
                case 2: playerStats[0].magicPowerAbilityPoints++; playerStats[0].statsPointsAvailable--; break;
                case 3: playerStats[0].agilityAbilityPoints++; playerStats[0].statsPointsAvailable--; break;
            }
        }
        playerStats[0].calculateStats();
        RefreshPlayerStatsUI();
    }
}
