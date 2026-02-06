using UnityEngine;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    public ShopItemUI itemPrefab;
    public Transform contentParent;
    public ShopItemData[] items;
    public TextMeshProUGUI coinText;

    private void OnEnable()
    {
        BuildShop();
        UpdateCoins();
    }

    void BuildShop()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (ShopItemData item in items)
        {
            ShopItemUI ui = Instantiate(itemPrefab, contentParent);
            ui.Setup(item);
        }
    }
    public void UpdateCoins()
    {
        coinText.text = CoinSystem.Instance.Coins.ToString();
    }
}
