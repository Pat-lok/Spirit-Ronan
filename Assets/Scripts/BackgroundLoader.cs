using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    public Renderer backgroundRenderer;
    public Material[] backgroundMaterials;

    private const string BG_KEY = "SelectedBackground";

    void Start()
    {
        LoadBackground();
    }

    public void LoadBackground()
    {
        int index = PlayerPrefs.GetInt(BG_KEY, 0);

        if (index < 0 || index >= backgroundMaterials.Length)
            index = 0;

        backgroundRenderer.material = backgroundMaterials[index];
    }
}

