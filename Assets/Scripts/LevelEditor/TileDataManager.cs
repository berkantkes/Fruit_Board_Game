using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDataManager : MonoBehaviour
{
    [SerializeField] List<TileDataEditorController> _tileDatas;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _resetButton;

    private CollectionWrapper<SavedTileData> _savedItemData = new CollectionWrapper<SavedTileData>(new List<SavedTileData>());

    private void Start()
    {
        GetSavedItemData();
        InitializeDatas();
    }

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(SaveData);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(SaveData);
    }

    private void GetSavedItemData()
    {
        _savedItemData = JSONHelper.LoadData<CollectionWrapper<SavedTileData>>(JSONHelper.TileDataFilePath) ?? new CollectionWrapper<SavedTileData>(new List<SavedTileData>());
    }

    private void InitializeDatas()
    {
        for (int i = 0; i < _tileDatas.Count; i++)
        {
            _tileDatas[i].SetTileNumber(i + 1);

            if (i < _savedItemData.Items.Count && _savedItemData.Items[i] != null)
            {
                _tileDatas[i].SetRewardCount(_savedItemData.Items[i].RewardCount);
                _tileDatas[i].SetRewardType(_savedItemData.Items[i].RewardType);
            }

            _tileDatas[i].UpdateText();
        }
    }

    public void SaveData()
    {
        CreateData();
        JSONHelper.SaveData(JSONHelper.TileDataFilePath, _savedItemData);
    }

    public void ResetData()
    {
        JSONHelper.ResetData(JSONHelper.TileDataFilePath, new CollectionWrapper<SavedTileData>(new List<SavedTileData>()));
    }

    private void CreateData()
    {
        _savedItemData.Items.Clear();
        for (int i = 0; i < _tileDatas.Count; i++)
        {
            SavedTileData savedTileData = new SavedTileData();
            savedTileData.RewardType = _tileDatas[i].GetRewardsType();
            savedTileData.RewardCount = _tileDatas[i].GetRewardCount();
            _savedItemData.Items.Add(savedTileData);
        }
    }
}
