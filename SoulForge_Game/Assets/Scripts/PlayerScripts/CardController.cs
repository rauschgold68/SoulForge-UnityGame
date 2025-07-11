using SoulForge;
using UnityEngine;

/// <summary>
/// CardController manages all card-based upgrades for the player.
/// 
/// Usage:
/// - Attach this script to the player or a persistent upgrade manager.
/// - Call the provided Add*Upgrade methods to apply stat upgrades when a card is collected or activated.
/// - The upgrade stage is tracked and can be queried for context-sensitive logic.
/// 
/// Example:
///     cardController.AddDamageUpgrade("FirstUpgrade");
///     cardController.AddAttackRangeUpgrade("SecondUpgrade");
/// </summary>
public class CardController : StatController
{
    // Private field for range upgrade stage
    private string _rangeUpgradeStage = "Default";

    /// <summary>
    /// Gets or sets the current attack range upgrade stage.
    /// </summary>
    public string RangeUpgradeStage
    {
        get => _rangeUpgradeStage;
        set => _rangeUpgradeStage = value;
    }

    /// <summary>
    /// Upgrades the player's attack range based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier (e.g. "FirstUpgrade")</param>
    public void AddAttackRangeUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                SetAttackRange(1f);
                RangeUpgradeStage = "FirstUpgrade";
                break;
            case "SecondUpgrade":
                SetAttackRange(1.7f);
                RangeUpgradeStage = "SecondUpgrade";
                break;
            case "ThirdUpgrade":
                SetAttackRange(2.5f);
                RangeUpgradeStage = "ThirdUpgrade";
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    /// <summary>
    /// Returns the current attack range upgrade stage.
    /// </summary>
    public string GetRangeUpgradeStage()
    {
        return RangeUpgradeStage;
    }

    /// <summary>
    /// Upgrades the player's max and current health based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier</param>
    public void AddHealthUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                AddMaxHealth(50);
                AddCurrentHealth(50); // Fixed casing
                break;
            case "SecondUpgrade":
                AddMaxHealth(150);
                AddCurrentHealth(150); // Fixed casing
                break;
            case "ThirdUpgrade":
                AddMaxHealth(300);
                AddCurrentHealth(300); // Fixed casing
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }

    /// <summary>
    /// Upgrades the player's damage based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier</param>
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

    /// <summary>
    /// Upgrades the player's movement speed based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier</param>
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

    /// <summary>
    /// Upgrades the player's attack speed based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier</param>
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

    /// <summary>
    /// Enables life steal for the player based on the given upgrade type.
    /// </summary>
    /// <param name="upgradeType">Upgrade stage identifier</param>
    public void AddLifeStealUpgrade(string upgradeType)
    {
        switch (upgradeType)
        {
            case "FirstUpgrade":
                EnableLifeSteal(upgradeType);
                break;
            case "SecondUpgrade":
                EnableLifeSteal(upgradeType);
                break;
            case "ThirdUpgrade":
                EnableLifeSteal(upgradeType);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }
}