using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _appleText;
    [SerializeField] private TextMeshProUGUI _pearText;
    [SerializeField] private TextMeshProUGUI _strawberryText;

    private int _appleCount = 0;
    private int _pearCount = 0;
    private int _strawberryCount = 0;

    private SavedItemData _savedItemData = new SavedItemData();

    public void Initialize()
    {
        LoadSavedItemData();
        StartValues();
    }

    private void LoadSavedItemData()
    {
        _savedItemData = JSONHelper.LoadData<SavedItemData>(JSONHelper.InventoryFilePath);
    }

    private void OnEnable()
    {
        EventManager<NodeController>.Subscribe(GameEvents.OnMoveEnd, AddInventory);
    }

    private void StartValues()
    {
        if (_savedItemData != null)
        {
            _appleCount = _savedItemData.AppleCount;
            _appleText.SetText(_appleCount.ToString());

            _pearCount = _savedItemData.PearCount;
            _pearText.SetText(_pearCount.ToString());

            _strawberryCount = _savedItemData.StrawberryCount;
            _strawberryText.SetText(_strawberryCount.ToString());
        }
        else
        {
            Debug.LogWarning("No saved data found, starting with default values.");
        }
    }

    private void AddInventory(NodeController controller)
    {
        if (controller.GetRewardType() == RewardsType.Apple)
        {
            _appleCount += controller.GetRewardCount();
            _appleText.SetText(_appleCount.ToString());
            _savedItemData.AppleCount = _appleCount;
        }
        else if (controller.GetRewardType() == RewardsType.Pear)
        {
            _pearCount += controller.GetRewardCount();
            _pearText.SetText(_pearCount.ToString());
            _savedItemData.PearCount = _pearCount;
        }
        else if (controller.GetRewardType() == RewardsType.Strawberry)
        {
            _strawberryCount += controller.GetRewardCount();
            _strawberryText.SetText(_strawberryCount.ToString());
            _savedItemData.StrawberryCount = _strawberryCount;
        }

        JSONHelper.SaveData(JSONHelper.InventoryFilePath, _savedItemData);
    }
}

[System.Serializable]
public class SavedItemData
{
    public int AppleCount = 0;
    public int PearCount = 0;
    public int StrawberryCount = 0;
}
