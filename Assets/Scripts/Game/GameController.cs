using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DiceGroupController _diceGroupController;
    [SerializeField] private BoardController _boardController;
    [SerializeField] private SpriteSelector _prefabSelector;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private UIManager _uiManager;

    private List<SavedTileData> _savedItemData = new List<SavedTileData>();

    private void Start()
    {
        Application.targetFrameRate = 60;
        LoadTileData();
        _prefabSelector.Initialize();
        _diceGroupController.Initialize();
        _playerController.Initialize(_boardController);
        _uiManager.Initialize();
        CreateMap();
    }

    private void LoadTileData()
    {
        _savedItemData = JSONHelper.LoadData<CollectionWrapper<SavedTileData>>(JSONHelper.TileDataFilePath)?.Items ?? new List<SavedTileData>();
    }

    private void CreateMap()
    {
        _boardController.NodeController[0].SetRewardType(RewardsType.None, 0);

        for (int i = 1; i < _boardController.NodeController.Count; i++)
        {
            _boardController.NodeController[i].SetRewardType(_savedItemData[i - 1].RewardType, _savedItemData[i - 1].RewardCount);
        }
    }

    public void ResetTileData()
    {
        JSONHelper.ResetData(JSONHelper.TileDataFilePath, new CollectionWrapper<SavedTileData>(new List<SavedTileData>()));
    }
}
[System.Serializable]
public class SavedTileData
{
    public RewardsType RewardType;
    public int RewardCount;
}