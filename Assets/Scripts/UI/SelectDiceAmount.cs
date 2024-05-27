using TMPro;
using UnityEngine;

public class SelectDiceAmount : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private ValueWrapper<int> _valueWrapper = new ValueWrapper<int>(1);

    public void Initialize()
    {
        LoadDropdownValue();
        SetDropdownValue(_valueWrapper.Value);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetDropdownValue);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetDropdownValue);
    }

    private void SetDropdownValue(int value)
    {
        _valueWrapper.Value = value;
        SaveDropdownValue();
        EventManager<int>.Execute(GameEvents.OnChangeDiceAmount, _valueWrapper.Value + 1);
    }

    private void SaveDropdownValue()
    {
        JSONHelper.SaveData(JSONHelper.DiceAmountFilePath, _valueWrapper);
    }

    private void LoadDropdownValue()
    {
        ValueWrapper<int> savedValueWrapper = JSONHelper.LoadData<ValueWrapper<int>>(JSONHelper.DiceAmountFilePath);
        if (savedValueWrapper != null)
        {
            _valueWrapper = savedValueWrapper;
            _dropdown.value = _valueWrapper.Value;
            Debug.Log("Dropdown value loaded: " + _valueWrapper.Value);
        }
        else
        {
            _valueWrapper.Value = 0;
            Debug.Log("No saved dropdown value found. Setting to default: " + _valueWrapper.Value);
        }
    }

    private void ResetData()
    {
        JSONHelper.ResetData(JSONHelper.DiceAmountFilePath, _valueWrapper);
    }
}
