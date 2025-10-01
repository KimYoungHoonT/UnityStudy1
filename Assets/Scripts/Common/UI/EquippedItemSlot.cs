using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemSlot : MonoBehaviour
{
    public Image addIcon;
    public Image equippedItemIcon;

    private UserItemData m_equippedItemData;

    public void SetItem(UserItemData userItemData)
    { 
        m_equippedItemData = userItemData;
        addIcon.gameObject.SetActive(false);
        equippedItemIcon.gameObject.SetActive(true);

        StringBuilder sb = new StringBuilder(m_equippedItemData.ItemId.ToString());
        sb[1] = '1';
        var itemIconName = sb.ToString();

        var itemIconTexture = Resources.Load<Texture2D>($"{Define.Textures_PATH}/{itemIconName}");
        if (itemIconTexture != null)
        {
            equippedItemIcon.sprite = Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(1f, 1f));
        }
    }

    public void ClearItem()
    {
        m_equippedItemData = null;

        addIcon.gameObject.SetActive(true);
        equippedItemIcon.gameObject.SetActive(false);
    }   

    public void OnClickEquippedItemSlot()
    {
        EquipmentUIData uiData = new EquipmentUIData();
        uiData.SerialNumber = m_equippedItemData.SerialNumber;
        uiData.ItemId = m_equippedItemData.ItemId;
        uiData.IsEquipped = true;

        UIManager.Instance.OpenUI<EquipmentUI>(uiData);
    }
}
