using UnityEngine;

public class Booster : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _boostSpeed;

    #endregion

    #region Unity Event Functions

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GameCharacter>(out var gameCharacter))
        {
            gameCharacter.Rigidbody.AddForce(Vector3.forward * _boostSpeed, ForceMode.VelocityChange);
            Destroy(gameObject);
        }
    }

    #endregion
}
