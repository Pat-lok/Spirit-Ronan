using UnityEngine;
using TMPro;

public class BombSystem : MonoBehaviour
{
    public static BombSystem Instance;

    public int bombCount = 5;
    public TextMeshProUGUI bombText;
    public GameObject explosionPrefab;


    private void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void UseBomb()
    {
        if (bombCount <= 0) return;

        bombCount--;
        UpdateUI();

        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        foreach (EnemyBase enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        Instantiate(explosionPrefab, Camera.main.transform.position + Vector3.forward * 5f, Quaternion.identity);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.bombExplosion);
    }

    void UpdateUI()
    {
        bombText.text = bombCount.ToString();
    }
}
