using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public static LifeSystem Instance;

    [Header("Lives")]
    public int maxLives = 5;
    public Image[] hearts;

    private int currentLives;
    private int missedEnemies = 0;
    public int missThreshold = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        currentLives = maxLives;
        UpdateUI();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void EnemyMissed()
    {
        missedEnemies++;

        if (missedEnemies >= missThreshold)
        {
            missedEnemies = 0;
            LoseLife();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives;
        }
    }

    void GameOver()
{
    GameOverManager.Instance.ShowGameOver();
}
public void ResetLives()
{
    currentLives = maxLives;
    missedEnemies = 0;
    UpdateUI();
}

}

