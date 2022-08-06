using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    #region Enums

    public enum UpgradeType
    {
        Speed,
        OfflineCoin,
    }

    #endregion

    #region Constants
    
    private const int BaseUpgradeCostSpeed = 30;
    private const int BaseUpgradeCostOfflineCoin = 50;
    
    private const int UpgradeCostPerLevelSpeed = 18;
    private const int UpgradeCostPerLevelOfflineCoin = 35;

    #endregion

    #region Fields

    [SerializeField] private Sprite _speedIcon;
    [SerializeField] private Sprite _offlineCoin;

    #endregion

    #region Public Methods

    public Sprite GetUpgradeIcon(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Speed       => _speedIcon,
            UpgradeType.OfflineCoin => _offlineCoin,
            _                       => null,
        };
    }
    
    public int GetUpgradeLevel(UpgradeType type)
    {
        var player = Game.Instance.Player;
        
        return type switch
        {
            UpgradeType.OfflineCoin   => player.OfflineCoinLevel,
            UpgradeType.Speed         => player.SpeedLevel,
            _                         => 0,
        };
    }

    public int GetUpgradeMaxLevel(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Speed        => 5,
            UpgradeType.OfflineCoin  => 5,
            _                        => 0,
        };
    }

    public int GetUpgradeCost(UpgradeType type)
    {
        var level = GetUpgradeLevel(type);
        
        return type switch
        {
            UpgradeType.Speed        => BaseUpgradeCostSpeed +  level * (level + 1) / 2 * UpgradeCostPerLevelSpeed,
            UpgradeType.OfflineCoin  => BaseUpgradeCostOfflineCoin + level * (level + 1) / 2 * UpgradeCostPerLevelOfflineCoin,
            _                        => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public bool CanBuyUpragde(UpgradeType type)
        => Game.Instance.Coin >= GetUpgradeCost(type)
         && GetUpgradeLevel(type) < GetUpgradeMaxLevel(type);

    public void UpgradebyType(UpgradeType type)
    {
        var game = Game.Instance;
        var player = Game.Instance.Player;
        
        game.RemoveCoin(GetUpgradeCost(type));
        player.SetUpgradeLevelByType(type, GetUpgradeLevel(type) + 1);
    }
    
    #endregion

    #region Unity Event Functions

    protected void Awake()
    {
        GameUI.Instance.MainMenu.Initialize(this);
    }

    #endregion
}
