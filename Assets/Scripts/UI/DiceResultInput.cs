using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceResultInput : MonoBehaviour
{
    private TMP_InputField inputField;
    private int _value = 1;

    public void Initialize()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.text = "1";
    }

    private void OnEnable()
    {
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void OnDisable()
    {
        inputField.onValueChanged.RemoveListener(ValidateInput);
    }

    void ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            _value = 0;
            return;
        }

        if (!int.TryParse(input, out int value))
        {
            inputField.text = "";
            _value = 0;
            return;
        }
        else if (value < 1)
        {
            inputField.text = "1";
            _value = 1;
        }
        else if (value > 6)
        {
            inputField.text = "6";
            _value = 6;
        }
        else
        {
            _value = value;
        }

    }

    public int GetValue()
    {
        return _value;
    }
}
