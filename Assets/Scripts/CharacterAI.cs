using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAI : MonoBehaviour
{
    #region Constante

    private const float MaxRaycastDistance = 7f;

    #endregion
    
    #region Fields

    [SerializeField] private float _moveSpeed;


    private float _lastUpdateAITime;
    private float _lastUpdateAIRandomTime;

    private float _randomWait;
    
    private RaycastHit _hit;

    #endregion

    #region Properties

    public GameCharacter GameCharacter { get; private set; }

    #endregion

    #region Unity Event Functions

    protected void Awake()
    {
        GameCharacter = GetComponent<GameCharacter>();
        GameCharacter.MoveSpeed = _moveSpeed;
        
        Game.Instance.StateChanged += GameOnStateChanged;
    }

    protected void Update()
    {
        if (!(Time.time - _lastUpdateAITime > 0.2f)) return;
        
        if (Physics.Raycast(transform.position, Vector3.forward, 
                out _hit, MaxRaycastDistance, Layer.MaskObstacle))
        {
            // if at lower part of the collider : Dive, else go up
            GameCharacter.IsDiving = transform.position.y < _hit.transform.position.y;
        }
        else
        {
            // Randomly Dive or go up if nothing ahead
            if (Time.time - _lastUpdateAIRandomTime > _randomWait)
            {
                _randomWait = Random.Range(0.5f, 1f);
                GameCharacter.IsDiving = Random.value < 0.5f;
                _lastUpdateAIRandomTime = Time.time;
            }
        }
        _lastUpdateAITime = Time.time;
    }

    protected void OnDestroy()
    {
        Game.Instance.StateChanged -= GameOnStateChanged;
    }

    #endregion

    #region Event Handler

    private void GameOnStateChanged(Game sender)
    {
        GameCharacter.CanMove = sender.State == GameState.Playing;
    }

    #endregion
}
