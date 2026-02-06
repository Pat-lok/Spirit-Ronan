using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Button selectButton;
    public GameObject selectedTag; // مثلا یه تیک یا نوشته Selected

    private ShopItemData data;

    private string OwnedKey => "OWNED_" + data.itemID;
    private string SelectedKey => "SELECTED_" + data.itemType;

    public void Setup(ShopItemData item)
    {
        data = item;

        icon.sprite = data.icon;
        priceText.text = data.price.ToString();

        buyButton.onClick.RemoveAllListeners();
        selectButton.onClick.RemoveAllListeners();

        buyButton.onClick.AddListener(Buy);
        selectButton.onClick.AddListener(Select);

        Refresh();
    }

    void Buy()
    {
        if (!CoinSystem.Instance.CanAfford(data.price))
            return;

        CoinSystem.Instance.SpendCoins(data.price);
        PlayerPrefs.SetInt(OwnedKey, 1);
        PlayerPrefs.Save();

        Refresh();
    }

    void Select()
    {
        PlayerPrefs.SetString(SelectedKey, data.itemID);

        // ذخیره index مناسب
        if (data.itemType == ShopItemType.Background)
            PlayerPrefs.SetInt("SelectedBackground", data.backgroundIndex);

        if (data.itemType == ShopItemType.BladeTrail)
            PlayerPrefs.SetInt("SelectedBladeTrail", data.bladeTrailIndex);

        PlayerPrefs.Save();

        RefreshAllItems();
    }

    void Refresh()
    {
        bool owned = data.isDefault || PlayerPrefs.GetInt(OwnedKey, 0) == 1;
        bool selected = PlayerPrefs.GetString(SelectedKey, "") == data.itemID;

        buyButton.gameObject.SetActive(!owned);
        selectButton.gameObject.SetActive(owned && !selected);

        if (selectedTag != null)
            selectedTag.SetActive(selected);
    }

    // 🔁 همه آیتم‌ها رو آپدیت کن (وقتی یکی Select میشه)
    void RefreshAllItems()
    {
        ShopItemUI[] items = transform.parent.GetComponentsInChildren<ShopItemUI>();
        foreach (var item in items)
            item.Refresh();
    }
}
