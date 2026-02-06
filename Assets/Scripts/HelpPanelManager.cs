using UnityEngine;
using UnityEngine.UI;

public class HelpPanelManager : MonoBehaviour
{
    public Image helpImage;
    public Sprite[] helpPages;

    private int currentIndex = 0;

    private void OnEnable()
    {
        currentIndex = 0;
        UpdateImage();
    }

    public void Next()
    {
        if (currentIndex < helpPages.Length - 1)
        {
            currentIndex++;
            UpdateImage();
        }
    }

    public void Prev()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        helpImage.sprite = helpPages[currentIndex];
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
