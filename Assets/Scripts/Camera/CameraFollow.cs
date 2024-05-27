using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private Vector3 offset;
    private float _centerZoomRatio = 1.5f;

    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position / _centerZoomRatio;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset,ref _currentVelocity, smoothTime);

    }
}
