using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public static CoinSystem Instance;

    private const string COIN_KEY = "PLAYER_COINS";
    private int coins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            coins = PlayerPrefs.GetInt(COIN_KEY, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Coins => coins;

    public bool CanAfford(int amount)
    {
        return coins >= amount;
    }

    public void AddCoins(int amount)
    {
        coins += Mathf.Max(0, amount);
        Save();
    }

    public bool SpendCoins(int amount)
    {
        if (!CanAfford(amount))
            return false;

        coins -= amount;
        Save();
        return true;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(COIN_KEY, coins);
        PlayerPrefs.Save();
    }
}
