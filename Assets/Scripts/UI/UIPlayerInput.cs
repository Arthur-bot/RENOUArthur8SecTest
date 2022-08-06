using UnityEngine;
using UnityEngine.EventSystems;

public class UIPlayerInput : MonoBehaviour
{
    #region Fields

    [SerializeField] private EventTrigger _trigger;
    
    private Vector2 _screenPosition;

    private GameCharacter _playerCharacter;

    #endregion

    #region Unity Event Functions

    protected void Awake()
    {
        var triggerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        triggerDown.callback.AddListener(OnPointerDown);
        _trigger.triggers.Add(triggerDown);

        var triggerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        triggerUp.callback.AddListener(OnPointerUp);
        _trigger.triggers.Add(triggerUp);
    }

    protected void Start()
    {
        _playerCharacter = Game.Instance.Player.GameCharacter;
    }

    #endregion

    #region Private Methods
    
    public void OnPointerDown(BaseEventData data)
    {
        if (_playerCharacter == null) return;

        _playerCharacter.IsDiving = true;
    }

    public void OnPointerUp(BaseEventData data)
    {
        if (_playerCharacter == null) return;

        _playerCharacter.IsDiving = false;
    }

    #endregion
}