using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    #region Fields

    [SerializeField] private UILevelInfo _uiLevelInfo;
    [SerializeField] private UIPlayerInput _playerInput;
    [SerializeField] private Image _fade;
    [SerializeField] private UIEndScreenPopup _endScreen;
    [SerializeField] private UIMainMenu _mainMenu;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private GameObject _coinGameObject;

    private Game _game;
    
    #endregion

    #region Properties

    public UILevelInfo UILevelInfo => _uiLevelInfo;

    public UIPlayerInput PlayerInput => _playerInput;

    public UIEndScreenPopup EndScreen => _endScreen;
    
    public UIMainMenu MainMenu => _mainMenu;
    
    public TextMeshProUGUI CoinText => _coinText;

    #endregion
    
    #region Public Methods

    public TweenerCore<Color, Color, ColorOptions> Fade(bool isIn, float duration = 0.3f)
        => _fade.DOFade(isIn ? 1f : 0f, duration)
            .SetEase(isIn ? Ease.OutSine : Ease.InQuart);

    #endregion
    
    #region Unity Event Functions

    protected override void OnAwake()
    {
        _game = Game.Instance;
        _game.StateChanged += GameOnStateChanged;
        
        CoinText.text = _game.Coin.ToString();
        Fade(false, 0.5f).From(1f);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        _game.StateChanged -= GameOnStateChanged;
    }

    #endregion
    
    #region Event Handler

    private void GameOnStateChanged(Game sender)
    {
        switch (sender.State)
        {
            case GameState.Playing:
                UILevelInfo.Text = "Level " + (Level.Current.Index + 1);
                PlayerInput.gameObject.SetActive(true);
                _coinGameObject.gameObject.SetActive(false);
                _uiLevelInfo.gameObject.SetActive(true);
                _uiLevelInfo.Progress = 0f;
                break;
            case GameState.Victory:
                _uiLevelInfo.gameObject.SetActive(false);
                _coinGameObject.gameObject.SetActive(false);
                break;
            case GameState.Defeat:
                _uiLevelInfo.gameObject.SetActive(false);
                _coinGameObject.gameObject.SetActive(false);
                break;
            case GameState.Menu:
                _mainMenu.ShowMenu(true);
                _uiLevelInfo.gameObject.SetActive(false);
                _coinGameObject.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion
}
