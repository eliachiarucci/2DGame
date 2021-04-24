using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Weapon2,
        Armor,
        Potion,
        QuestItem,
        Scroll,
        Generic
    }

    public ItemType itemType;

    public string itemName;
    public string description;
    public int price;
    public Sprite itemSprite;

    [Header("Item Details")]

    public int modifyHP;
    public int modifyMana, modifyMP, modifyStrength, modifyDodge, modifyBlock, modifyAgility, modifyStamina, modifyPhysicalDef, modifyMagicDef, modifyCriticalChance;
    public int weaponDamage, modifyMagicDamage, armorPhysicalDef, armorMagicDef;

    public bool equipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int targetChar)
    {
        CharStats selectedChar = GameManager.instance.playerStats[targetChar];
        if(itemType == ItemType.Scroll || itemType == ItemType.Potion)
        {
            int newHP = selectedChar.currentHP + modifyHP;
            if (newHP > selectedChar.maxHP) newHP = selectedChar.maxHP;
            if (newHP < 0) newHP = 0;
            selectedChar.currentHP = newHP;

            int newMana = selectedChar.currentMana + modifyMana;
            if (newMana > selectedChar.maxMana) newMana = selectedChar.maxMana;
            if (newMana < 0) newMana = 0;
            selectedChar.currentMana = newMana;

            selectedChar.bonusStrength += modifyStrength;

            selectedChar.bonusAgility += modifyAgility;

            selectedChar.bonusStamina += modifyStamina;

            selectedChar.bonusMagicPower += modifyMP;

            selectedChar.bonusCritChance += modifyCriticalChance;

            selectedChar.bonusPhysicalDefense += modifyPhysicalDef;

            selectedChar.bonusMagicDefense += modifyMagicDef;
        }
        selectedChar.calculateStats();
    }
}
