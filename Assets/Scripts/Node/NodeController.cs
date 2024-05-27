using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _countText;
    [SerializeField] private SpriteRenderer _iconSprite;

    private RewardsType _rewardType;
    private int _rewardCount = 0;

    public void SetRewardType(RewardsType rewardType, int rewardCount)
    {
        _rewardType = rewardType;
        _rewardCount = rewardCount;
        _countText.text = _rewardCount.ToString();

        if (rewardType == RewardsType.None)
        {
            _countText.SetText("");
            return;
        }

        _iconSprite.sprite = SpriteSelector.GetRewardPrefab(rewardType);
    }
    public RewardsType GetRewardType()
    {
        return _rewardType;
    }
    public int GetRewardCount()
    {
        return _rewardCount;
    }
}
