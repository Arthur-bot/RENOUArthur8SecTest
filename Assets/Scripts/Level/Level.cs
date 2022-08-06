using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector3 _spawnPosition;

    private int _index = -1;
    
    private UILevelInfo _levelInfo;
    private float _lastUILevelInfoRefreshTime;
    private Vector3 _initialDistancePlayerEnd;
    private float _totalDistanceFromSpawn;

    private PlayerInfo _player;

    #endregion
    
    #region Properties

    public static Level Current { get; private set; }

    public int Index => _index < 0
        ? _index = int.Parse(name) - 1
        : _index;
    
    public int CoinMultiplier { get; set; }
    
    public int CurrentCoin { get; set; }
    
    public int PlayerPosition { get; set; }
    
    public int NumberOfAIAheadOfPlayer { get; set; }

    public Vector3 SpawnPostion => _spawnPosition;

    #endregion
    
    #region Public Methods

    public static void Load(int levelIndex)
    {
        DestroyCurrent();

        var levelPrefab = GameResources.Instance.Levels[levelIndex];
        var level = Instantiate(levelPrefab);
        level.name = levelPrefab.name;
    }
    
    public static void DestroyCurrent()
    {
        if (Current != null)
        {
            DestroyImmediate(Current.gameObject);
            Current = null;
        }
    }

    public void EndLevel()
    {
        var game = Game.Instance;

        if (game.State != GameState.Playing) return;

        var victory = PlayerPosition <= 1;
        
        game.State = victory ? GameState.Victory : GameState.Defeat;

        var coins = victory ? CoinMultiplier * CurrentCoin : CurrentCoin;
        game.AddCoin(coins);
        GameUI.Instance.EndScreen.Show(victory, coins);
    }

    #endregion
    
    #region Unity Event Functions

    protected void Awake()
    {
        if (Current != null)
        {
            Destroy(gameObject);
            return;
        }

        Current = this;

        _totalDistanceFromSpawn = DistanceXZ(-_spawnPosition, GetComponentInChildren<EndTrigger>().transform.position);
        
        _player = Game.Instance.Player;
        _levelInfo = GameUI.Instance.UILevelInfo;
    }

    protected void Update()
    {
        if (Game.Instance.State != GameState.Playing) return;
        
        if (Time.time - _lastUILevelInfoRefreshTime > 0.2f)
        {
            var currentDistanceTraveled = DistanceXZ(_spawnPosition, _player.transform.position);
            _levelInfo.Progress = Mathf.Clamp(currentDistanceTraveled / _totalDistanceFromSpawn, 0f, 1f);
            
            _lastUILevelInfoRefreshTime = Time.time;
        }
    }

    #endregion

    #region Private Methods

    private float DistanceXZ(Vector3 a, Vector3 b)
    {
        a.x -= b.x;
        a.z -= b.z;

        return Mathf.Sqrt(a.x * a.x + a.z * a.z);
    }

    #endregion
}
