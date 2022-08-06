using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeItem : MonoBehaviour
{
    #region Fields
    
    [Header("Upgrade")]
    [SerializeField] private Upgrade.UpgradeType _type;

    [Header("UI")]
    [SerializeField] private Image _upgradeIcon;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;

    private Button _button;
    private Upgrade _target;
    private UIMainMenu _menu;

    #endregion

    #region Public Methods

    public void Initialize(Upgrade target, UIMainMenu menu)
    {
        _target = target;
        _menu = menu;
        _button = GetComponentInChildren<Button>();
        
        _button.onClick.AddListener(Upgrade);
        
        _upgradeIcon.sprite = _target.GetUpgradeIcon(_type);

        Refresh();
    }

    public void Refresh()
    {
        var isMax = _target.GetUpgradeLevel(_type) >= _target.GetUpgradeMaxLevel(_type);
        var canBuyUpgrade = _target.CanBuyUpragde(_type) && !isMax;

        _levelText.text = $"Lv {_target.GetUpgradeLevel(_type)}";
        _costText.text = isMax ? "Max" : _target.GetUpgradeCost(_type).ToString();
        
        _button.interactable = canBuyUpgrade;
    }

    #endregion

    #region Private Methods

    private void Upgrade()
    {
        _target.UpgradebyType(_type);
        _menu.Refresh();
    }

    #endregion
    
}