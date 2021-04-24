using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        QuestItem,
        Generic
    }

    public ItemType itemType;

    public string itemName;
    public string description;
    public int price;
    public Sprite itemSprite;

    [Header("Item Details")]

    public int modifyHP;
    public int modifyMana, modifyMP, modifyStrength, modifyDodge, modifyBlock, modifyAgility, modifyStamina, modifyPhysicalDef, modifyMagicDef;
    public int weaponDamage, armorPhysicalDef, armorMagicDef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
