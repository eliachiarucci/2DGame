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

}
