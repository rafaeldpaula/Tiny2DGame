using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    private const string CoinTag = "Coin";

    [SerializeField]
    private Text _text;

    private int _coins;

    private void Awake()
    {
        _coins = GetInitialCoinAmount();
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(CoinTag))
        {
            return;
        }

        CollectCoin(collision.gameObject);
    }

    private void CollectCoin(GameObject coin)
    {
        _coins++;
        UpdateCoinText();
        Destroy(coin);
    }

    private int GetInitialCoinAmount()
    {
        if (_text == null)
        {
            return 0;
        }

        return int.TryParse(_text.text, out int currentCoins) ? currentCoins : 0;
    }

    private void UpdateCoinText()
    {
        if (_text == null)
        {
            return;
        }

        _text.text = _coins.ToString();
    }
}
