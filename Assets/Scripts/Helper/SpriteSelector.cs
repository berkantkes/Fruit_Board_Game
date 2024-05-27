using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSelector : MonoBehaviour
{
    [SerializeField] private Sprite _applePrefab;
    [SerializeField] private Sprite _pearPrefab;
    [SerializeField] private Sprite _strawberryPrefab;

    private static Dictionary<RewardsType, Sprite> _rewardPrefabs;

    public void Initialize()
    {
        _rewardPrefabs = GetRewardPrefabs();
    }

    private Dictionary<RewardsType, Sprite> GetRewardPrefabs()
    {
        Dictionary<RewardsType, Sprite> rewardPrefabs = new Dictionary<RewardsType, Sprite>
        {
            [RewardsType.Apple] = _applePrefab,
            [RewardsType.Pear] = _pearPrefab,
            [RewardsType.Strawberry] = _strawberryPrefab,
        };

        return rewardPrefabs;
    }

    public static Sprite GetRewardPrefab(RewardsType rewardsType)
    {
        return _rewardPrefabs[rewardsType];
    }
}
