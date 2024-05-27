using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DiceResultInputCoordinator _diceResultInputCoordinator;
    [SerializeField] private InventoryController _inventoryController;
    [SerializeField] private SelectDiceAmount _selectDiceAmount;
    public void Initialize()
    {
        _diceResultInputCoordinator.Initialize();
        _inventoryController.Initialize();
        _selectDiceAmount.Initialize();
    }
}
