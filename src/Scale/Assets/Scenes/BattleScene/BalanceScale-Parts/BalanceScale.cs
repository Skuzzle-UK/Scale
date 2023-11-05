using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 90f)]
    private float _maxRotationDegrees = 20f;

    [SerializeField]
    [Range(10f, 100f)]
    private float _weightForMaxRotation = 50f;

    [SerializeField]
    [ReadOnly]
    private float _weight;

    private float _stepSize;

    // Treat like a constructor
    private void Awake()
    {
        _weight = 0f;
        _stepSize = _maxRotationDegrees / _weightForMaxRotation;
    }

    // weight is positive to rotate clockwise and negative to rotate anti-clockwise
    public void AddWeight(float weight)
    {
        _weight += weight;
        var newRotationAngle = _stepSize * _weight;
        transform.Rotate(Vector3.forward, newRotationAngle);
    }
}
