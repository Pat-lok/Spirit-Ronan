using UnityEngine;

public enum ShopItemType
{
    Background,
    BladeTrail
}

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItemData : ScriptableObject
{
    public string itemID;
    public ShopItemType itemType;
    

    public Sprite icon;
    public int price;
    public bool isDefault;

    [Header("Apply Data")]
    public int backgroundIndex;   // فقط برای Background
    public int bladeTrailIndex;   // فقط برای BladeTrail
}
