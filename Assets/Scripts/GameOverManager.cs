using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI")]
    public GameObject overlay;
    public GameObject panel;
    public TextMeshProUGUI scoreText;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        overlay.SetActive(false);
        panel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0f;

        overlay.SetActive(true);
        panel.SetActive(true);

        // نمایش امتیاز
        scoreText.text = "Score: " + ScoreSystem.Instance.score;

        // صدا
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOverSound);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // 🔴 فعلاً Placeholder برای تبلیغ
    public void ContinueWithAd()
    {
        Debug.Log("Watch Ad → Continue (Later)");

        // بعداً:
        // TapSell.ShowRewardedAd(...)
    }

    // 🔥 اینو بعداً تپسل صدا می‌زنه
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        overlay.SetActive(false);
        panel.SetActive(false);
        isGameOver = false;

        LifeSystem.Instance.ResetLives();
    }
}
