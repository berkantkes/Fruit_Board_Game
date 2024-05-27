using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollButton : MonoBehaviour
{
    [SerializeField] private Button _diceRollButton;

    public void OnEnable()
    {
        _diceRollButton.onClick.AddListener(OnRollButtonClicked);
        EventManager<NodeController>.Subscribe(GameEvents.OnMoveEnd, SetClickableRollButton);
    }

    public void OnDisable()
    {
        _diceRollButton.onClick.RemoveListener(OnRollButtonClicked);
        EventManager<NodeController>.Unsubscribe(GameEvents.OnMoveEnd, SetClickableRollButton);
    }

    private void OnRollButtonClicked()
    {
        EventManager.Execute(GameEvents.OnDiceRollButton);
        _diceRollButton.enabled = false;
    }
    private void SetClickableRollButton(NodeController controller)
    {
        _diceRollButton.enabled = true;
    }

}
