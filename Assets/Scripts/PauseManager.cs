using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("UI")]
    public GameObject pauseOverlay;          // بک‌گراند تیره
    public PanelAnimator pausePanel;          // پنل با انیمیشن

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        pauseOverlay.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // ⏸️ دکمه Pause
    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;

        pauseOverlay.SetActive(true);
        pausePanel.Show();
    }

    public void Resume()
    {
        if (!isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;

        pausePanel.Hide();
        pauseOverlay.SetActive(false);
    }

    // 🔁 Start Over
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🏠 Home
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // 🔇 صدا
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
}
