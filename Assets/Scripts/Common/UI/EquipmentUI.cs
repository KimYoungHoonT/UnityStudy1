using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIData : BaseUIData
{
    public long SerialNumber;
    public int ItemId;
    public bool IsEquipped;
}

public class EquipmentUI : BaseUI
{
    public Image _itemGradeBG;
    public Image _itemIcon;
    public TextMeshProUGUI _itemGradeText;
    public TextMeshProUGUI _itemNameText;
    public TextMeshProUGUI _attackPowerAmountText;
    public TextMeshProUGUI _defenseAmountText;

    private EquipmentUIData m_equipmentUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_equipmentUIData = uiData as EquipmentUIData;
        if (m_equipmentUIData == null)
        {
            Logger.LogError("m_equipmentUIData is invalid");
            return;
        }

        ItemData itemData = DataTableManager.Instance.GetItemData(m_equipmentUIData.ItemId);
        if (itemData == null)
        {
            Logger.LogError($"itemData is invalid. ItemId : {m_equipmentUIData.ItemId}");
            return;
        }

        // 21001 / 1000 % 10 => 1 => common
        ItemGrade itemGrade = (ItemGrade)((m_equipmentUIData.ItemId / 1000) % 10);
        
        Texture2D gradeBgTexture = Resources.Load<Texture2D>($"Texture/{itemGrade}"); // Texture/common
        if (gradeBgTexture != null)
        {
            _itemGradeBG.sprite = Sprite.Create(gradeBgTexture, new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height), new Vector2(1f, 1f));
        }

        // 등급의 텍스트 설정
        _itemGradeText.text = itemGrade.ToString();

        string hexColor = string.Empty;
        switch (itemGrade)
        {
            case ItemGrade.Common:
                hexColor = "#1AB3FF";
                break;
            case ItemGrade.Uncommon:
                hexColor = "#51C52C";
                break;
            case ItemGrade.Rare:
                hexColor = "#EA5AFF";
                break;
            case ItemGrade.Epic:
                hexColor = "#FF9900";
                break;
            case ItemGrade.Legendary:
                hexColor = "#F24949";
                break;
            default:
                break;
        }

        // 등급의 텍스트 컬러 설정
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            _itemGradeText.color = color;
        }

        StringBuilder sb = new StringBuilder(m_equipmentUIData.ItemId.ToString());
        sb[1] = '1'; // ex) _itemId 가 만약 22001 이라면, 아이템 파일이름과 일치시키기 위해 2번째 자리를 1로 전부 바꿔줌 => 2'1'001 
        string itemIconName = sb.ToString();

        Texture2D itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
        if (itemIconTexture != null)
        {
            _itemIcon.sprite = Sprite.Create(itemIconTexture, new Rect(0, 0, itemIconTexture.width, itemIconTexture.height), new Vector2(1f, 1f));
        }

        _itemNameText.text = itemData.ItemName;
        _attackPowerAmountText.text = $"+{itemData.AttackPower}";
        _defenseAmountText.text = $"+{itemData.Defense}";
    }
}
