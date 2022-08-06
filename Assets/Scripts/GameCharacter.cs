using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    #region Constante

    private const float BaseSpeedCoef = 4f;

    #endregion
    
    #region Fields

    [Header("Movements")] 
    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _maxRotation;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Constraints")] 
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    
    private float _moveY;
    private bool _canMove;

    #endregion

    #region Properties

    public float BaseMoveSpeed => _baseMoveSpeed;

    public float MoveSpeed { get; set; }

    public bool CanMove
    {
        get => _canMove;
        set
        {
           if (_canMove == value) return;

           _canMove = value;

           if (!value)
           {
               _rigidbody.velocity = Vector3.zero;
               _rigidbody.angularVelocity = Vector3.zero;
               _rigidbody.inertiaTensor = Vector3.zero;
           }
        }
    }

    public bool IsDiving { get; set; }

    public Rigidbody Rigidbody => _rigidbody;

    #endregion

    #region Unity Event Functions

    protected void Update()
    {
        if (!CanMove) return;

        // Smooth rotation
        var targetRotation = Quaternion.Euler(IsDiving ? _maxRotation : -_maxRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        var xRotation = transform.eulerAngles.x > 180f ? transform.eulerAngles.x - 360f : transform.eulerAngles.x;
        
        // calculate speed bonus
        var speedCoef = Mathf.InverseLerp(-_maxRotation.x, _maxRotation.x, xRotation);
        var speedBonus = Mathf.Clamp(speedCoef * BaseSpeedCoef, 1, BaseSpeedCoef);

        // If player touching bounds remove speed bonus
        if (transform.localPosition.y <= _minY || transform.localPosition.y >= _maxY)
        {
            speedBonus = 1f;
        }
        
        var nextPosition = transform.localPosition + transform.forward * speedBonus * MoveSpeed * Time.deltaTime;
        nextPosition.y = Mathf.Clamp(nextPosition.y, _minY, _maxY);

        transform.localPosition = nextPosition;
    }

    protected void LateUpdate()
    {
        if (!CanMove) return;

        var velocity = _rigidbody.velocity;
        velocity.y = 0;
        velocity.x = 0;

        _rigidbody.velocity = velocity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.inertiaTensor = Vector3.zero;
    }

    #endregion
}
