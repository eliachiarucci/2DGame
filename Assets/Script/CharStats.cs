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
    public int physicalDefense;
    public int magicalDefense;

    public int statsPointsAvailable = 0;

    public int weapon;
    public int armor;

    public string equippedWeapon;
    public string equippedArmor;

    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        calculateExpForLevels();
        calculatePlayerLevel();
        calculateAbilityStats();
        calculateMaxBaseStats();
        calculateStatsAbilityToSpend();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AddExp(300);
        }
    }

    void newLevel(int newLevel)
    {
        if(playerLevel != 0)
        {
            Debug.Log("NEW LEVEL!");
        }
        playerLevel = newLevel;
        calculateAbilityStats();
        calculateMaxBaseStats();
        calculateStatsAbilityToSpend();
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
