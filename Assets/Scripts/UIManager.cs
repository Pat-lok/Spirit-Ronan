using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 🔓 باز کردن پنل
    public void OpenPanel(GameObject panel)
    {
        if (panel == null) return;

        panel.SetActive(true);

        PanelAnimator animator = panel.GetComponent<PanelAnimator>();
        if (animator != null)
            animator.Show();
    }

    // 🔒 بستن پنل
    public void ClosePanel(GameObject panel)
    {
        if (panel == null) return;

        PanelAnimator animator = panel.GetComponent<PanelAnimator>();
        if (animator != null)
            animator.Hide();
        else
            panel.SetActive(false);
    }

    // 🔄 بستن همه پنل‌ها (اختیاری)
    public void CloseAllPanels(PanelAnimator[] panels)
    {
        foreach (var p in panels)
            if (p != null)
                p.Hide();
    }
}
