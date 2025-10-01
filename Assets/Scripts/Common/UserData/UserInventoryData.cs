using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

[Serializable]
public class UserItemData
{
    public long SerialNumber; // 고유 넘버
    public int ItemId;

    public UserItemData(long serialNumber, int itemId)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
    }
}

[Serializable]
public class UserInventoryItemDataListWrapper  // 모델의 용도 즉 JSON 변환 용도
{
    public List<UserItemData> InventoryItemDataList;
}

public class UserInventoryData : IUserData
{
    public UserItemData EquippedWeaponData { get; set; }
    public UserItemData EquippedShieldData { get; set; }
    public UserItemData EquippedChestArmorData { get; set; }
    public UserItemData EquippedBootsData { get; set; }
    public UserItemData EquippedGlovesData { get; set; }
    public UserItemData EquippedAccessoryData { get; set; }

    public List<UserItemData> InventoryItemDataList { get; set; } = new List<UserItemData>(); // 진짜 우리가 게임에서 사용할 용도

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        // 나중엔 네트워크를 통해서 실제 서버에 저장된 데이터 가져와서 등록하기

        //serealNumber => DateTime.Now.ToString("yyyyMMddHHmmss") + Random.Range(0, 9999).ToString("D4")
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 11001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 11002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 22001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 22002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 33001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 43002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 44001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 44002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 55001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 55002));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 65001));
        InventoryItemDataList.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + UnityEngine.Random.Range(0, 9999).ToString("D4")), 65002));

        EquippedWeaponData = new UserItemData(InventoryItemDataList[0].SerialNumber, InventoryItemDataList[0].ItemId);
        EquippedShieldData = new UserItemData(InventoryItemDataList[2].SerialNumber, InventoryItemDataList[2].ItemId);
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false; // 플래그

        try
        {
            string weaponJson = PlayerPrefs.GetString("EquippedWeaponData");
            if (string.IsNullOrEmpty(weaponJson) == false)
            {
                EquippedWeaponData = JsonUtility.FromJson<UserItemData>(weaponJson);
                Logger.Log($"EquippedWeaponData : SN : {EquippedWeaponData.SerialNumber}, ItemId : {EquippedWeaponData.ItemId}");
            }

            string shieldJson = PlayerPrefs.GetString("EquippedShieldData");
            if (string.IsNullOrEmpty(shieldJson) == false)
            {
                EquippedShieldData = JsonUtility.FromJson<UserItemData>(shieldJson);
                Logger.Log($"EquippedShiledData : SN : {EquippedShieldData.SerialNumber}, ItemId : {EquippedShieldData.ItemId}");
            }

            string chestArmorJson = PlayerPrefs.GetString("EquippedChestArmorData");
            if (string.IsNullOrEmpty(chestArmorJson) == false)
            {
                EquippedChestArmorData = JsonUtility.FromJson<UserItemData>(chestArmorJson);
                Logger.Log($"EquippedWeaponData : SN : {EquippedChestArmorData.SerialNumber}, ItemId : {EquippedChestArmorData.ItemId}");
            }

            string BootsJson = PlayerPrefs.GetString("EquippedBootsData");
            if (string.IsNullOrEmpty(BootsJson) == false)
            {
                EquippedBootsData = JsonUtility.FromJson<UserItemData>(BootsJson);
                Logger.Log($"EquippedBootsData : SN : {EquippedBootsData.SerialNumber}, ItemId : {EquippedBootsData.ItemId}");
            }

            string GlovesJson = PlayerPrefs.GetString("EquippedGlovesData");
            if (string.IsNullOrEmpty(GlovesJson) == false)
            {
                EquippedGlovesData = JsonUtility.FromJson<UserItemData>(GlovesJson);
                Logger.Log($"EquippedGlovesData : SN : {EquippedGlovesData.SerialNumber}, ItemId : {EquippedGlovesData.ItemId}");
            }

            string AccessoryJson = PlayerPrefs.GetString("EquippedAccessoryData");
            if (string.IsNullOrEmpty(AccessoryJson) == false)
            {
                EquippedAccessoryData = JsonUtility.FromJson<UserItemData>(AccessoryJson);
                Logger.Log($"EquippedAccessoryData : SN : {EquippedAccessoryData.SerialNumber}, ItemId : {EquippedAccessoryData.ItemId}");
            }

            string inventoryItemDataListJson = PlayerPrefs.GetString("InventoryItemDataList");
            if (string.IsNullOrEmpty(inventoryItemDataListJson) == false)
            {
                UserInventoryItemDataListWrapper itemDataListWrapper = JsonUtility.FromJson<UserInventoryItemDataListWrapper>(inventoryItemDataListJson);
                InventoryItemDataList = itemDataListWrapper.InventoryItemDataList;

                Logger.Log("InventoryItemDataList Lode");
                foreach (var item in InventoryItemDataList)
                {
                    Logger.Log($"serialNumber : {item.SerialNumber}, itemId : {item.ItemId}");
                }
            }

            result = true;
        }
        catch (Exception e)
        {
            Logger.LogError($"Load failed ({e.Message})");
        }

        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false; // 플래그

        try
        {
            string weaponJson = JsonUtility.ToJson(EquippedWeaponData);
            PlayerPrefs.SetString("EquippedWeaponData", weaponJson);
            if (EquippedWeaponData != null)
            {
                Logger.Log($"EquippedWeaponData: SN : {EquippedWeaponData.SerialNumber}, ItemID : {EquippedWeaponData.ItemId}");
            }

            string shieldJson = JsonUtility.ToJson(EquippedShieldData);
            PlayerPrefs.SetString("EquippedShieldData", shieldJson);
            if (EquippedShieldData != null)
            {
                Logger.Log($"EquippedShieldData: SN : {EquippedShieldData.SerialNumber}, ItemID : {EquippedShieldData.ItemId}");
            }

            string chestArmorJson = JsonUtility.ToJson(EquippedChestArmorData);
            PlayerPrefs.SetString("EquippedChestArmorData", chestArmorJson);
            if (EquippedChestArmorData != null)
            {
                Logger.Log($"EquippedChestArmorData: SN : {EquippedChestArmorData.SerialNumber}, ItemID : {EquippedChestArmorData.ItemId}");
            }

            string BootsJson = JsonUtility.ToJson(EquippedBootsData);
            PlayerPrefs.SetString("EquippedBootsData", BootsJson);
            if (EquippedBootsData != null)
            {
                Logger.Log($"EquippedBootsData: SN : {EquippedBootsData.SerialNumber}, ItemID : {EquippedBootsData.ItemId}");
            }

            string GlovesJson = JsonUtility.ToJson(EquippedGlovesData);
            PlayerPrefs.SetString("EquippedGlovesData", GlovesJson);
            if (EquippedGlovesData != null)
            {
                Logger.Log($"EquippedGlovesData: SN : {EquippedGlovesData.SerialNumber}, ItemID : {EquippedGlovesData.ItemId}");
            }

            string AccessoryJson = JsonUtility.ToJson(EquippedAccessoryData);
            PlayerPrefs.SetString("EquippedAccessoryData", AccessoryJson);
            if (EquippedAccessoryData != null)
            {
                Logger.Log($"EquippedAccessoryData: SN : {EquippedAccessoryData.SerialNumber}, ItemID : {EquippedAccessoryData.ItemId}");
            }

            UserInventoryItemDataListWrapper itemDataListWrapper = new UserInventoryItemDataListWrapper();
            itemDataListWrapper.InventoryItemDataList = InventoryItemDataList;
            string inventoryItemDataListJson = JsonUtility.ToJson(itemDataListWrapper);
            PlayerPrefs.SetString("InventoryItemDataList", inventoryItemDataListJson);

            Logger.Log("InventoryItemDataList Save");
            foreach (var item in InventoryItemDataList)
            {
                Logger.Log($"serialNumber : {item.SerialNumber}, itemId : {item.ItemId}");
            }

            result = true;
        }
        catch (Exception e)
        {
            Logger.LogError($"Save failed ({e.Message})");
        }

        return result;
    }
}
