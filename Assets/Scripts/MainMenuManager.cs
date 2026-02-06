using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject darkOverlay;
    public PanelAnimator shopPanel;
    public PanelAnimator helpPanel;
    public PanelAnimator settingsPanel;

    private void Start()
    {
        darkOverlay.SetActive(false);
    }

    // ▶️ Start Game
    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    // 🛒 Shop
    public void OpenShop()
    {
        OpenPanel(shopPanel);
    }

    // ❓ Help
    public void OpenHelp()
    {
        OpenPanel(helpPanel);
    }

    // ⚙️ Settings
    public void OpenSettings()
    {
        OpenPanel(settingsPanel);
    }

    // ❌ Close current panel
    public void CloseAllPanels()
    {
        darkOverlay.SetActive(false);
        shopPanel.Hide();
        helpPanel.Hide();
        settingsPanel.Hide();
    }

    void OpenPanel(PanelAnimator panel)
    {
        CloseAllPanels();
        darkOverlay.SetActive(true);
        panel.Show();
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
