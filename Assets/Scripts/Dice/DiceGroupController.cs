using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceGroupController : MonoBehaviour
{
    [SerializeField] private List<SingleDiceController> _singleDiceController;
    [SerializeField] private DiceResultInputCoordinator _diceResultInputCoordinator;
    [SerializeField] private PlayerController _playerController;

    private List<DiceResultInput> DiceResultInputList;

    private int _diceAmount;

    public void Initialize()
    {
        DiceResultInputList = _diceResultInputCoordinator.GetDiceResultInputList();

        foreach (var controller in _singleDiceController)
        {
            controller.Initialize(this);
        }
    }

    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.OnDiceRollButton, OnRollButtonClicked);
        EventManager<int>.Subscribe(GameEvents.OnChangeDiceAmount, OnChangeDiceAmount);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.OnDiceRollButton, OnRollButtonClicked);
        EventManager<int>.Unsubscribe(GameEvents.OnChangeDiceAmount, OnChangeDiceAmount);
    }

    private void OnRollButtonClicked()
    {
        RollDice();
    }
    private void OnChangeDiceAmount(int diceAmount)
    {
        _diceAmount = diceAmount;

        foreach (var controller in _singleDiceController)
        {
            controller.gameObject.SetActive(false);
        }

        for (int i = 0; i < _diceAmount; i++)
        {
            _singleDiceController[i].gameObject.SetActive(true);
        }
    }

    private void RollDice()
    {
        for (int i = 0; i < _diceAmount; i++)
        {
            _singleDiceController[i].GetAnimator().SetTrigger(DiceResultInputList[i].GetValue() + "v" + UnityEngine.Random.Range(1, 4));
            _singleDiceController[i].StartAnimation();
        }
    }

    public void EndAnimations()
    {
        int totalIntValue = 0;

        for(int i = 0; i < _diceAmount; i++)
        {
            totalIntValue += DiceResultInputList[i].GetValue();
        }

        if (_singleDiceController.All(dice => !dice.GetIsAnimationPlaying()))
        {
            _playerController.MovePlayer(totalIntValue);
        }
    }



}