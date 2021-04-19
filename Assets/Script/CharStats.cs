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

    public int currentMana;
    public int maxMana;

    private int statsStartingValue = 10;
    public int stamina;
    public int strength;
    public int magicPower;
    public int agility;
    public int physicalDefense;
    public int magicalDefense;

    public int block;
    public int dodge;
    public int critChance;

    public int statsPointsAvailable = 0;

    public int weaponDamage;

    public string equippedWeapon;
    public string equippedArmor;

    public Sprite charImage;

    void Start()
    {
        calculateExpForLevels();
        calculatePlayerLevel();
        calculateAbilityStats();
        calculateMaxBaseStats();
        calculateStatsAbilityToSpend();
    }

    void newLevel(int newLevel)
    {
        if(playerLevel != 0) //If it is loading the exp for the first time the playerLevel will be 0, we don't want to trigger any new level functions.
        {
            //newLevel
        }
        playerLevel = newLevel;
        calculateAbilityStats();
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

    public void calculateAbilityStats()
    {
        stamina = statsStartingValue;
        strength = statsStartingValue;
        magicPower = statsStartingValue;
        agility = statsStartingValue;
        physicalDefense = statsStartingValue;
        magicalDefense = statsStartingValue;
    }

    public void calculateMaxBaseStats()
    {
        int bonusHealthAtFirstLevel = Mathf.FloorToInt((1 * 1 * 100) / 25);
        int bonusHealth = Mathf.FloorToInt((playerLevel * playerLevel * 100) / 25);
        maxHP = Mathf.FloorToInt(100 + bonusHealth - bonusHealthAtFirstLevel);
    }

    public void calculateStatsAbilityToSpend()
    {
        int currentStatsAbilitySpent = (stamina-statsStartingValue) + (strength-statsStartingValue) + (magicPower-statsStartingValue) + (physicalDefense-statsStartingValue) + (magicalDefense-statsStartingValue);
        int statsAbilityAcquired = playerLevel - 1;
        statsPointsAvailable = statsAbilityAcquired - currentStatsAbilitySpent;
    }

}
