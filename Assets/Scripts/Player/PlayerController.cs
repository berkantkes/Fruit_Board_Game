using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpAudio;

    private BoardController _boardController;
    private int _boardIndex = 0;
    private AudioSource _jumpAudioSource;

    public void Initialize(BoardController boardController)
    {
        _boardController = boardController;
        LoadBoardIndex();
        SetInitialPositionAndRotation();
        _jumpAudioSource = GetComponent<AudioSource>();
    }

    private void LoadBoardIndex()
    {
        _boardIndex = JSONHelper.LoadData<BoardIndexData>(JSONHelper.BoardIndexFilePath)?.boardIndex ?? 0;
    }

    private void SetInitialPositionAndRotation()
    {
        if (IsValidBoardIndex(_boardIndex))
        {
            transform.position = GetTargetPosition(_boardIndex);
            transform.rotation = GetYRotation(_boardIndex);
        }
        else
        {
            Debug.LogError("Board index out of range or NodeController list is empty.");
        }
    }

    private bool IsValidBoardIndex(int index)
    {
        return _boardController != null &&
               _boardController.NodeController != null &&
               index >= 0 && index < _boardController.NodeController.Count;
    }

    private Vector3 GetTargetPosition(int index)
    {
        return _boardController.NodeController[index].transform.position + new Vector3(0, 0.1f, 0);
    }

    private Quaternion GetYRotation(int index)
    {
        float yRotation = (index / 7) * 90 % 360;
        return Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
    }

    public void MovePlayer(int stepCount)
    {
        StartCoroutine(MoveAndRotate(stepCount));
    }

    private IEnumerator MoveAndRotate(int stepCount)
    {
        float moveDuration = 0.3f;

        while (stepCount > 0)
        {
            stepCount--;
            UpdateBoardIndex();

            if (IsValidBoardIndex(_boardIndex))
            {
                Vector3 startPosition = transform.position;
                Vector3 targetPosition = GetTargetPosition(_boardIndex);
                Quaternion startRotation = transform.rotation;
                Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x - 45, transform.eulerAngles.y, transform.eulerAngles.z);

                bool rotateYAxis = _boardIndex % 7 == 0;
                Quaternion yAxisTargetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);

                EventManager<FakeNodeData>.Execute(GameEvents.OnMoveStart, new FakeNodeData(_boardIndex, moveDuration));
                PlayJumpSound();

                yield return MoveCharacter(startPosition, targetPosition, startRotation, targetRotation, yAxisTargetRotation, rotateYAxis, moveDuration);
            }
            else
            {
                Debug.LogError("Board index out of range or NodeController list is empty.");
                yield break;
            }
        }

        EventManager<NodeController>.Execute(GameEvents.OnMoveEnd, _boardController.NodeController[_boardIndex]);
    }

    private void PlayJumpSound()
    {
        _jumpAudioSource.PlayOneShot(_jumpAudio);
    }

    private void UpdateBoardIndex()
    {
        _boardIndex++;
        if (_boardIndex >= _boardController.NodeController.Count)
        {
            _boardIndex = 0;
        }
        SaveData(_boardIndex);
    }

    private IEnumerator MoveCharacter(Vector3 startPosition, Vector3 targetPosition, Quaternion startRotation, 
        Quaternion targetRotation, Quaternion yAxisTargetRotation, bool rotateYAxis, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            float jump = Mathf.Sin(Mathf.PI * t) * 0.8f;
            transform.position = new Vector3(transform.position.x, startPosition.y + jump, transform.position.z);

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (rotateYAxis)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, yAxisTargetRotation, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = rotateYAxis ? yAxisTargetRotation : startRotation;
    }

    public void SaveData(int data)
    {
        JSONHelper.SaveData(JSONHelper.BoardIndexFilePath, new BoardIndexData { boardIndex = data });
    }

    public void ResetBoardIndex()
    {
        JSONHelper.ResetData(JSONHelper.BoardIndexFilePath, new BoardIndexData { boardIndex = 0 });
    }
}

[System.Serializable]
public class BoardIndexData
{
    public int boardIndex;
}
