using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultInputCoordinator : MonoBehaviour
{
    [SerializeField] private List<DiceResultInput> _diceResultInputList;

    private int _diceCount = 0;

    public void Initialize()
    {
        foreach (var diceResultInput in _diceResultInputList)
        {
            diceResultInput.Initialize();
        }
    }

    private void OnEnable()
    {
        EventManager<int>.Subscribe(GameEvents.OnChangeDiceAmount, OnChangeDiceAmount);
    }

    private void OnChangeDiceAmount(int value)
    {
        _diceCount = value;

        foreach (var diceResultInput in _diceResultInputList)
        {
            diceResultInput.gameObject.SetActive(false);
        }

        for (int i = 0; i < _diceCount; i++)
        {
            _diceResultInputList[i].gameObject.SetActive(true);
        }
    }

    public List<DiceResultInput> GetDiceResultInputList() 
    { 
        return _diceResultInputList; 
    }
}
