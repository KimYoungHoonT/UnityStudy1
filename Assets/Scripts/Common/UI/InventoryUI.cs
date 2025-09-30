using Gpm.Ui;
using UnityEngine;

public class InventoryUI : BaseUI
{
    public InfiniteScroll InventoryScrollList;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetInventory();
    }

    private void SetInventory()
    {
        InventoryScrollList.Clear();

        UserInventoryData userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData != null)
        {
            foreach (var item in userInventoryData.InventoryItemDataList)
            {
                var itemSlotData = new InventoryItemSlotData();
                itemSlotData.SerialNumber = item.SerialNumber;
                itemSlotData.ItemId = item.ItemId;
                InventoryScrollList.InsertData(itemSlotData);
            }
        }
    }
}
