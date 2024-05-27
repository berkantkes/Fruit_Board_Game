using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TileDataEditorController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardName;
    [SerializeField] private TextMeshProUGUI _tileNumberText;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_InputField _rewardCountInput;
    [SerializeField] private Image _tileBackground;

    private RewardsType _rewardType = RewardsType.None;
    private int _rewardCount = 0;
    private int _tileNumber = 0;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        _previousButton.onClick.AddListener(BackRewardType);
        _nextButton.onClick.AddListener(NextRewardType);
        _rewardCountInput.onValueChanged.AddListener(ValidateInput);
    }
    private void OnDisable()
    {
        _previousButton.onClick.RemoveListener(BackRewardType);
        _nextButton.onClick.RemoveListener(NextRewardType);
        _rewardCountInput.onValueChanged.RemoveListener(ValidateInput);
    }

    void ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        if (!int.TryParse(input, out int value))
        {
            _rewardCountInput.text = "";
            return;
        }
        else if (value < 1)
        {
            _rewardCountInput.text = "1";
        }
        else if (value > 999)
        {
            _rewardCountInput.text = "999";
        }
        _rewardCount = value;
    }

    private void BackRewardType()
    {
        if (_rewardType == 0)
        {
            return;
        }
        _rewardType--;

        UpdateText();
        UpdateColor();
    }
    private void NextRewardType()
    {
        if ((int)_rewardType == 3)
        {
            return;
        }
        _rewardType++;

        UpdateText();
        UpdateColor();
    }

    public void UpdateText()
    {
        _rewardName.SetText(_rewardType.ToString());
        _rewardCountInput.text = _rewardCount.ToString();
        _tileNumberText.SetText(_tileNumber.ToString());
    }

    private void UpdateColor()
    {
        switch(_rewardType)
        {
            case RewardsType.None:
                _tileBackground.color = Color.white;
                break;
            case RewardsType.Apple:
                _tileBackground.color = Color.red;
                break;
            case RewardsType.Strawberry:
                _tileBackground.color = new Color(1f, 0f, 0.6667f);
                break;
            case RewardsType.Pear:
                _tileBackground.color = Color.green;
                break;

        }
    }

    public void SetRewardType(RewardsType rewardsType)
    {
        _rewardType = rewardsType;
        UpdateColor();
    }
    public void SetRewardCount(int count)
    {
        _rewardCount = count;
    }
    public void SetTileNumber(int number)
    {
        _tileNumber = number;
    }

    public RewardsType GetRewardsType()
    {
        return _rewardType;
    }

    public int GetRewardCount()
    {
        return _rewardCount;
    }

}
