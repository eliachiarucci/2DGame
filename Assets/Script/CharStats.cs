using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 0;
    public int currentExp;

    public List<int> expToNextLevel = new List<int>();
    private int maxLevel = 25;

    public int currentHP;
    public int maxHP;
    public int bonusHP;

    public int currentMana;
    public int maxMana;
    public int bonusMana;

    private int statsStartingValue = 10;
    public int stamina;
    public int bonusStamina;
    public int strength;
    public int bonusStrength;
    public int magicPower;
    public int bonusMagicPower;
    public int agility;
    public int bonusAgility;
    public int physicalDefense;
    public int bonusPhysicalDefense;
    public int magicalDefense;
    public int bonusMagicDefense;

    public int staminaAbilityPoints;
    public int strengthAbilityPoints;
    public int agilityAbilityPoints;
    public int magicPowerAbilityPoints;

    public int block;
    public int bonusBlock;
    public int dodge;
    public int bonusDodge;
    public int critChance;
    public int bonusCritChance;

    public int statsPointsAvailable = 0;

    public int weaponDamage;
    public int magicDamage;
    public int bonusMagicDamage;

    public string equippedFirstHand;
    public string equippedSecondHand;
    public string equippedArmor;

    public Sprite charImage;

    void Start()
    {
        calculateExpForLevels();
        calculatePlayerLevel();
        calculateStats();
        calculateMaxBaseStats();
        calculateStatsAbilityToSpend();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AddExp(800);
        }
    }

    void newLevel(int newLevel)
    {
        if(playerLevel != 0) //If it is loading the exp for the first time the playerLevel will be 0, we don't want to trigger any new level functions.
        {
            //newLevel
        }
        playerLevel = newLevel;
        calculateStats();
        calculateMaxBaseStats();
        calculateStatsAbilityToSpend();
        GameMenu.instance.UpdateMainStats();
    }

    void calculateExpForLevels()
    {
        for (int i = 1; i < maxLevel; i++)
        {
            expToNextLevel.Add(i * i * 100);
        }
    }

    void calculatePlayerLevel()
    {
        int expRequired = 0;
        for (int i = 0; i < expToNextLevel.Count; i++)
        {
            expRequired += expToNextLevel[i];
            if (currentExp < expRequired)
            {
                if (playerLevel < i + 1)
                {
                    newLevel(i + 1);
                }
                return;
            }
        }
        if (playerLevel < maxLevel)
        {
            newLevel(maxLevel);
        }
        return;
    }

    public int[] getCurrentLevelExp()
    {
        int totalExpRequiredForNextLevel = 0;
        int totalExpRequiredBeforeThisLevel = 0;
        int[] expArray;
        for (int i = 0; i < expToNextLevel.Count; i++)
        {
            totalExpRequiredForNextLevel += expToNextLevel[i];
            if (currentExp < totalExpRequiredForNextLevel)
            {
                totalExpRequiredBeforeThisLevel = totalExpRequiredForNextLevel - expToNextLevel[i];
                int expRequiredForThisLevel = totalExpRequiredForNextLevel - totalExpRequiredBeforeThisLevel;
                int playerExpInThisLevel = currentExp - totalExpRequiredBeforeThisLevel;
                expArray = new int[] {playerExpInThisLevel, expRequiredForThisLevel };
                return expArray;
            }
        }
        expArray = new int[] { 0, 0 };
        return expArray;
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;
        calculatePlayerLevel();
    }

    public void calculateStats()
    {
        bonusHP = 0;
        bonusMana = 0;
        weaponDamage = 0;
        magicDamage = 0;
        bonusStamina = 0;
        bonusStrength = 0;
        bonusAgility = 0;
        bonusMagicPower = 0;
        bonusPhysicalDefense = 0;
        bonusMagicDefense = 0;
        bonusDodge = 0;
        bonusBlock = 0;
        bonusCritChance = 0;
        Item[] equippedArray = { GameManager.instance.getItemDetails(equippedFirstHand), GameManager.instance.getItemDetails(equippedSecondHand), GameManager.instance.getItemDetails(equippedArmor)};
        foreach (Item item in equippedArray)
        {
            if (item != null)
            {
                Debug.Log(item.modifyDodge);
                Debug.Log(item.itemName);
                bonusHP += item.modifyHP;
                bonusMana += item.modifyMana;
                weaponDamage += item.weaponDamage;
                magicDamage += item.modifyMagicDamage;
                bonusStamina += item.modifyStamina;
                bonusStrength += item.modifyStrength;
                bonusAgility += item.modifyAgility;
                bonusMagicPower += item.modifyMP;
                bonusPhysicalDefense += item.modifyPhysicalDef;
                bonusMagicDefense += item.modifyMagicDef;
                bonusDodge += item.modifyDodge;   
                bonusBlock += item.modifyBlock;
                bonusCritChance += item.modifyCriticalChance;
            }
        }
        maxHP = 100 + bonusHP;
        maxMana = 25 + bonusMana;
        stamina = statsStartingValue + staminaAbilityPoints + bonusStamina;
        strength = statsStartingValue + strengthAbilityPoints + bonusStrength;
        magicPower = statsStartingValue + magicPowerAbilityPoints + bonusMagicPower;
        agility = statsStartingValue + agilityAbilityPoints + bonusAgility;
        physicalDefense = bonusPhysicalDefense;
        magicalDefense = bonusMagicDefense;
        magicDamage += bonusMagicDamage;
        block = bonusBlock;
        dodge = bonusDodge;
        critChance = bonusCritChance;
        CalculateAbilityPointStats();
    }

    public void CalculateAbilityPointStats()
    {   
        weaponDamage = weaponDamage + CalculateBonus(weaponDamage, strength, 10) + CalculateBonus(weaponDamage, strength, 10);
        if (weaponDamage < 0) weaponDamage = 0;

        maxHP = maxHP + CalculateBonus(maxHP, strength, 5) + CalculateBonus(maxHP, stamina, 10);

        critChance = critChance + CalculatePureBonus(strength, 1) + CalculatePureBonus(agility, 2);
        if (critChance < 0) critChance = 0;

        maxMana = maxMana + CalculateBonus(maxMana, stamina, 5) + CalculateBonus(maxMana, magicPower, 10);
        if (maxMana < 0) maxMana = 0;

        physicalDefense = physicalDefense + CalculateBonus(physicalDefense, stamina, 5);
        if (physicalDefense < 0) physicalDefense = 0;

        magicalDefense = magicalDefense + CalculateBonus(magicalDefense, stamina, 5) + CalculateBonus(magicalDefense, magicPower, 5);
        if (magicalDefense < 0) magicalDefense = 0;

        magicDamage = magicDamage + CalculateBonus(magicDamage, magicPower, 5);
        if (magicDamage < 0) magicDamage = 0;

        dodge = dodge + CalculatePureBonus(agility, 2);
        if (dodge < 0) dodge = 0;
    }

    private int CalculateBonus(int originalValue, int stat, int percentage)
    {
        return Mathf.FloorToInt((((stat - statsStartingValue) * percentage) * originalValue) / 100);
    }

    private int CalculatePureBonus(int stat, int percentage)
    {
        return (stat - statsStartingValue) * percentage;
    }

    public void calculateMaxBaseStats()
    {
        int bonusHealthAtFirstLevel = Mathf.FloorToInt((1 * 1 * 100) / 25);
        int bonusHealth = Mathf.FloorToInt((playerLevel * playerLevel * 100) / 25);
        bonusHP = Mathf.FloorToInt(bonusHealth - bonusHealthAtFirstLevel);
    }

    public void calculateStatsAbilityToSpend()
    {
        int currentStatsAbilitySpent = staminaAbilityPoints + strengthAbilityPoints + magicPowerAbilityPoints + agilityAbilityPoints;
        int statsAbilityAcquired = playerLevel - 1;
        statsPointsAvailable = statsAbilityAcquired - currentStatsAbilitySpent;
    }

}
