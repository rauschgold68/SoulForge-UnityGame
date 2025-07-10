using SoulForge;
using UnityEngine;

public class CardController : StatController
{
    public string rangeUpgradeStage = "Default";

    public void SetAttackRangeUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                SetAttackRange(1f);
                rangeUpgradeStage = "FirstUpgrade";
                break;
            case "SecondUpgrade":
                SetAttackRange(1.7f);
                rangeUpgradeStage = "SecondUpgrade";
                break;
            case "ThirdUpgrade":
                SetAttackRange(2.5f);
                rangeUpgradeStage = "ThirdUpgrade";
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    public string GetRangeUpgradeStage()
    {
        return rangeUpgradeStage;
    }

    public void AddHealthUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                AddMaxHealth(50);
                AddcurrentHealth(50);
                break;
            case "SecondUpgrade":
                AddMaxHealth(150);
                AddcurrentHealth(150);
                break;
            case "ThirdUpgrade":
                AddMaxHealth(300);
                AddcurrentHealth(300);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    public void AddDamageUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                AddDamage(15);
                break;
            case "SecondUpgrade":
                AddDamage(25);
                break;
            case "ThirdUpgrade":
                AddDamage(40);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    public void AddSpeedUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                AddMoveSpeed(0.5f);
                break;
            case "SecondUpgrade":
                AddMoveSpeed(1f);
                break;
            case "ThirdUpgrade":
                AddMoveSpeed(1.5f);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    public void AddAttackSpeedUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                AddAttackSpeed(0.2f);
                break;
            case "SecondUpgrade":
                AddAttackSpeed(0.4f);
                break;
            case "ThirdUpgrade":
                AddAttackSpeed(0.6f);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    public void AddLifeStealUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                EnableLifeSteal(true);
                break;
            case "SecondUpgrade":
                EnableLifeSteal(true, 1);
                break;
            case "ThirdUpgrade":
                EnableLifeSteal(true, 2);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }
}