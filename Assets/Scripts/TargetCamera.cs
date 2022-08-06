using UnityEngine;

public class TargetCamera : Singleton<TargetCamera>
{
    #region Fields

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;

    #endregion

    #region Properties

    public Transform Target { get; set; }

    #endregion

    #region Unity Event Functions

    protected override void OnAwake()
    {
        base.OnAwake();

        Target = Game.Instance.Player.transform;
    }

    protected void LateUpdate()
    {
        if (Target == null) return;

        var targetPosition = Target.position + _offset;
        var position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed);

        transform.position = position;
    }

    #endregion
}