using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    #region MyRegion

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInfo>(out var player))
        {
            GameUI.Instance.PlayerInput.gameObject.SetActive(false);
            player.GameCharacter.IsDiving = true;
            Level.Current.PlayerPosition = Level.Current.NumberOfAIAheadOfPlayer + 1;
        }
        else if(other.TryGetComponent<CharacterAI>(out var character))
        {
            character.GameCharacter.IsDiving = true;
            Level.Current.NumberOfAIAheadOfPlayer += 1;
        }
    }

    #endregion
}
