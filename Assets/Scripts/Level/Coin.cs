using UnityEngine;

public class Coin : MonoBehaviour
{
    #region Unity Event Functions

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInfo>(out _))
        {
            Level.Current.CurrentCoin += 1;
            Instantiate(GameResources.Instance.CoinFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    #endregion
}
