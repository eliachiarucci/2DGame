using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CharStats[] playerStats;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItem;
    public bool dialogActive, gameMenuOpen, fadingBetweenAreas;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        GameMenu.instance.closeMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogActive || gameMenuOpen || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        } else
        {
            PlayerController.instance.canMove = true;
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("blabla");
            AddItem("Health Potion");
            RemoveItem("Mana Potion");
        }
    }

    public Item getItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItem.Length; i++)
        {
            if (referenceItem[i].itemName == itemToGrab)
            {
                return referenceItem[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "")
            {
                for(int currentSlot = i+1; currentSlot < itemsHeld.Length; currentSlot++)
                {
                    if(itemsHeld[currentSlot] != "")
                    {
                        itemsHeld[i] = itemsHeld[currentSlot];
                        itemsHeld[currentSlot] = "";
                        numberOfItems[i] = numberOfItems[currentSlot];
                        numberOfItems[currentSlot] = 0;
                        break;
                    }
                }
            }
        }
    }

    public void EquipItem(string itemToEquip)
    {
        Item item = getItemDetails(itemToEquip);

        if(item.itemType == Item.ItemType.Armor)
        {
            UnEquip(playerStats[0].equippedArmor);
            playerStats[0].equippedArmor = itemToEquip;
            item.equipped = true;
        }

        if(item.itemType == Item.ItemType.Weapon2)
        {
            UnEquip(playerStats[0].equippedFirstHand);
            UnEquip(playerStats[0].equippedSecondHand);
            playerStats[0].equippedFirstHand = itemToEquip;
            playerStats[0].equippedSecondHand = itemToEquip;
            item.equipped = true;
        }

        if (item.itemType == Item.ItemType.Weapon)
        {
            if(playerStats[0].equippedFirstHand == "")
            {
                UnEquip(playerStats[0].equippedFirstHand);
                playerStats[0].equippedFirstHand = itemToEquip;
                item.equipped = true;
            } else
            {
                UnEquip(playerStats[0].equippedSecondHand);
                playerStats[0].equippedSecondHand = itemToEquip;
                item.equipped = true;
            }            
        }

        playerStats[0].calculateStats();

    }

    public void UnEquip(string itemToUnEquip)
    {
        if(itemToUnEquip != "")
        {
            Item item = getItemDetails(itemToUnEquip);
            switch(item.itemType)
            {
                case Item.ItemType.Armor:
                    playerStats[0].equippedArmor = ""; break;
                case Item.ItemType.Weapon:
                        if (playerStats[0].equippedFirstHand == item.itemName) 
                        { 
                            playerStats[0].equippedFirstHand = ""; 
                        } else if (playerStats[0].equippedSecondHand == item.itemName) 
                        {
                            playerStats[0].equippedSecondHand = "";
                        }
                        break;
                case Item.ItemType.Weapon2:
                    playerStats[0].equippedFirstHand = "";
                    playerStats[0].equippedSecondHand = "";
                    break;
            }
            item.equipped = false;
        }

        playerStats[0].calculateStats();

    }

    public void AddItem(string itemToAdd)
    {
        bool validItem = false;

        for(int i = 0; i < referenceItem.Length; i++)
        {
            if(referenceItem[i].itemName == itemToAdd)
            {
                validItem = true;
            }
        }

        if (validItem)
        {
            int maxStack = 10;
            Item equippedItem = GameManager.instance.getItemDetails(itemToAdd);

            if(equippedItem.itemType == Item.ItemType.Weapon ||
               equippedItem.itemType == Item.ItemType.Weapon2 ||
               equippedItem.itemType == Item.ItemType.Armor)
            {
                maxStack = 1;
            }

            for (int i = 0; i < itemsHeld.Length; i++)
            {
                if (itemsHeld[i] == itemToAdd)
                {
                    if(numberOfItems[i] >= maxStack)
                    {
                        Debug.LogError("Maximum number of items reached");
                        break;
                    }
                    numberOfItems[i] = numberOfItems[i] + 1;
                    break;
                }

                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemToAdd;
                    numberOfItems[i] = 1;
                    break;
                }

            }
        } else
        {
            Debug.LogError("Item does not exist");
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                numberOfItems[i] = numberOfItems[i] - 1;
                if(numberOfItems[i] == 0)
                {
                    itemsHeld[i] = "";
                }
                break;
            }
        }
        GameMenu.instance.ShowItems();
    }
}
