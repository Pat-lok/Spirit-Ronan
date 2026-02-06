using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem Instance;

    public Image comboImage;
    public float comboShowTime = 0.6f;

    private int sliceCount = 0;
    private bool slicingActive = false;

    private void Awake()
    {
        Instance = this;
        comboImage.gameObject.SetActive(false);
    }

    public void StartSlice()
    {
        sliceCount = 0;
        slicingActive = true;
    }

    public void RegisterKill()
    {
        if (!slicingActive) return;
        sliceCount++;
    }

    public void EndSlice()
    {
        if (sliceCount >= 2)
        {
            ScoreSystem.Instance.AddScore(10);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.comboSound);
            StartCoroutine(ShowCombo());
        }

        slicingActive = false;
    }

    IEnumerator ShowCombo()
    {
        comboImage.gameObject.SetActive(true);
        // comboText.text = "COMBO!";
        yield return new WaitForSeconds(comboShowTime);
        comboImage.gameObject.SetActive(false);
    }
}
