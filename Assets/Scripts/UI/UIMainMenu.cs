using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button _playButton;
    [SerializeField] private List<UIUpgradeItem> _upgradeItems;

    #endregion

    #region Public Methods

    public void ShowMenu(bool show)
    {
        gameObject.SetActive(show);
        
        if (show)
        {
            Refresh();
        }
    }

    public void Initialize(Upgrade target)
    {
        _playButton.onClick.AddListener(() =>
        {
            Game.Instance.State = GameState.Playing;
            ShowMenu(false);
        });
        foreach (var item in _upgradeItems)
        {
            item.Initialize(target, this);
        }
    }
    
    public void Refresh()
    {
        foreach (var item in _upgradeItems)
        {
            item.Refresh();
        }
    }

    #endregion
}