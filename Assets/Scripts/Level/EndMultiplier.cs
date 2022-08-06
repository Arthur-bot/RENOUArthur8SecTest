using UnityEngine;

public class EndMultiplier : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _multiplier;

    #endregion

    #region Unity Event Functions

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerInfo>(out var player))
        {
            player.GameCharacter.CanMove = false;

            var level = Level.Current;
            level.CoinMultiplier = _multiplier;
            level.EndLevel();
            
        }
        else if(collision.gameObject.TryGetComponent<GameCharacter>(out var character))
        {
            character.CanMove = false;
        }
    }

    #endregion
}
