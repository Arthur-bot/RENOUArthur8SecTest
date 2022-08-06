using System;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    #region Properies
    
    public GameCharacter GameCharacter { get; private set; }

    public int SpeedLevel
    {
        get => PlayerPrefs.GetInt("SpeedLevel");
        set
        {
            if (SpeedLevel == value) return;

            PlayerPrefs.SetInt("SpeedLevel", value);
        }
    }
    
    public int OfflineCoinLevel     
    {
        get => PlayerPrefs.GetInt("OfflineCoinLevel");
        set
        {
            if (SpeedLevel == value) return;

            PlayerPrefs.SetInt("OfflineCoinLevel", value);
        }
    }

    #endregion

    #region Public Methods

    public void SetUpgradeLevelByType(Upgrade.UpgradeType type, int level)
    {
        switch (type)
        {
            case Upgrade.UpgradeType.OfflineCoin:
                OfflineCoinLevel = level;
                break;
            
            case Upgrade.UpgradeType.Speed:
                SpeedLevel = level;
                GameCharacter.MoveSpeed = GameCharacter.BaseMoveSpeed + SpeedLevel;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    #endregion

    #region Unity Event Functions

    protected void Awake()
    {
        GameCharacter = GetComponent<GameCharacter>();
        
        SetUpgradeLevelByType(Upgrade.UpgradeType.Speed, SpeedLevel);
        SetUpgradeLevelByType(Upgrade.UpgradeType.OfflineCoin, OfflineCoinLevel);
        
        Game.Instance.StateChanged += GameOnStateChanged;
    }
    
    protected void OnDestroy()
    {
        Game.Instance.StateChanged -= GameOnStateChanged;
    }

    #endregion

    #region Event Handlers

    private void GameOnStateChanged(Game sender)
    {
        GameCharacter.CanMove = sender.State == GameState.Playing;
    }

    #endregion
}
