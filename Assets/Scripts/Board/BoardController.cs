using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private List<NodeController> _nodeController = new List<NodeController>();
    [SerializeField] private GameObject _fakeGlowNodeItem;
    public List<NodeController> NodeController => _nodeController;

    private void OnEnable()
    {
        EventManager<FakeNodeData>.Subscribe(GameEvents.OnMoveStart, ShowFakeGlowNodeItem);
    }

    private void OnDisable()
    {
        EventManager<FakeNodeData>.Unsubscribe(GameEvents.OnMoveStart, ShowFakeGlowNodeItem);
    }
    private void ShowFakeGlowNodeItem(FakeNodeData fakeNodeData)
    {
        StartCoroutine(ActivateObjectForDuration(fakeNodeData));
    }

    private IEnumerator ActivateObjectForDuration(FakeNodeData fakeNodeData)
    {
        _fakeGlowNodeItem.SetActive(true);
        _fakeGlowNodeItem.transform.position = _nodeController[fakeNodeData.BoardIndex].transform.position;

        yield return new WaitForSeconds(fakeNodeData.MoveDuration - (fakeNodeData.MoveDuration / 5));

        _fakeGlowNodeItem.SetActive(false);
    }
}

