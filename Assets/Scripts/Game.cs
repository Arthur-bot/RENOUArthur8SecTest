using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum GameState
{
    Playing,
    Victory,
    Defeat,
    Menu
}

public class Game : Singleton<Game>
{
    #region Fields

    [SerializeField] private PlayerInfo _player;
    
    private GameState _state;

    #endregion

    #region Properties

    private int CurrentLevelIndex
    {
        get => PlayerPrefs.GetInt("CurrentLevelIndex");
        set
        {
            if (CurrentLevelIndex == value) return;

            PlayerPrefs.SetInt("CurrentLevelIndex", value);
        }
    }

    public int Coin
    {
        get => PlayerPrefs.GetInt("Coin");
        private set
        {
            if (Coin == value) return;

            PlayerPrefs.SetInt("Coin", value);
            GameUI.Instance.CoinText.text = Coin.ToString();
        }
    }

    public GameState State
    {
        get => _state;
        set
        {
            if (_state == value) return;

            _state = value;

            StateChanged?.Invoke(this);
        }
    }

    public PlayerInfo Player => _player;

    #endregion
    
    #region Event
    
    public delegate void EventHandler(Game sender);
    public event EventHandler StateChanged;

    #endregion

    #region Public Methods

    public void LoadLevel(int index)
    {
        // If no more levels, loop
        if (index >= GameResources.Instance.Levels.Count)
        {
            index = 0;
        }
        
        CurrentLevelIndex = index;
        
        StartCoroutine(Coroutine());
        IEnumerator Coroutine()
        {
            yield return GameUI.Instance.Fade(true).WaitForCompletion();

            yield return null;

            Level.Load(CurrentLevelIndex);

            _player.transform.position = Level.Current.SpawnPostion;
            _player.transform.rotation = Quaternion.identity;
            _player.GameCharacter.IsDiving = false;

            GameUI.Instance.Fade(false).SetDelay(0.5f); 
            
            State = GameState.Menu;
        }
    }

    public void AddCoin(int amount) => Coin += amount;
    
    public void RemoveCoin(int amount) => Coin -= amount;

    #endregion

    #region Unity Event Functions

    protected void Start()
    {
        State = GameState.Menu;

        LoadLevel(CurrentLevelIndex);
    }

    #endregion
}
