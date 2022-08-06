using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScreenPopup : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _standingText;
    [SerializeField] private GameObject _coinGameObject;
    [SerializeField] private TextMeshProUGUI _coinCount;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _retryButton;

    #endregion

    #region Public Methods

    public void Show(bool victory, int coin = 0)
    {
        _header.text = victory ? "Level\n<b><color=green>completed" : "Level\n<b><color=red>Failed";
        _nextButton.gameObject.SetActive(victory);
        _retryButton.gameObject.SetActive(!victory);

        _standingText.text = GetStandingtext(Level.Current.PlayerPosition);
        
        _coinCount.text = $"+{coin}";
        _coinGameObject.SetActive(coin > 0);
        
        gameObject.SetActive(true);
    }

    #endregion

    #region Protected Methods

    protected void Awake()
    {
        var game = Game.Instance;
        
        _nextButton.onClick.AddListener(() =>
        {
            game.LoadLevel(Level.Current.Index + 1);
            gameObject.SetActive(false);
        });
        
        _retryButton.onClick.AddListener(() =>
        {
            game.LoadLevel(Level.Current.Index);
            gameObject.SetActive(false);
        });
    }

    #endregion

    #region Private Methods

    private string GetStandingtext(int standing)
    {
        return standing switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            4 => "4th",
            _ => "None"
        };
    }

    #endregion
}
