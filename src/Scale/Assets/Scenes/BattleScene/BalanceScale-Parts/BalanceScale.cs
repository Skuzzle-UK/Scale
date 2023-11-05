using System;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    #region Event Handlers
    public event EventHandler WeightChangedEvent;
    #endregion

    #region Inspector Visible Properties
    [SerializeField]
    [Range(0f, 90f)]
    private float _maxRotationAngle = 20f;
    private float _minRotationAngle {  get { return -_maxRotationAngle; } }

    [SerializeField]
    [Range(10f, 100f)]
    private float _weightRequiredForMaxRotation = 50f;

    [SerializeField]
    [ReadOnly]
    private float _weight;
    #endregion

    #region Private Properties
    private float Weight
    {
        get { return _weight; }
        set
        {
            _weight = value;
            WeightChangedEvent.Invoke(this, new EventArgs());
        }
    }

    private float _stepSize;
    #endregion

    #region Unity Methods
    // Treat like a constructor
    private void Awake()
    {
        WeightChangedEvent += WeightChanged;
        Reset();
    }
    #endregion

    #region Class Methods
    // weight is positive to rotate clockwise and negative to rotate anti-clockwise
    public void AddWeight(float weight)
    {
        Weight += weight;
    }

    public void RemoveWeight(float weight)
    {
        Weight -= weight;
    }

    public void Reset()
    {
        Weight = 0f;
        _stepSize = _maxRotationAngle / _weightRequiredForMaxRotation;
    }

    private void WeightChanged(object sender, EventArgs e)
    {
        // NOTE: minus weight to force correct direction of rotation
        var newRotationAngle = _stepSize * -_weight;

        if (newRotationAngle > _maxRotationAngle)
        {
            newRotationAngle = _maxRotationAngle;
        }
        else if (newRotationAngle < _minRotationAngle)
        {
            newRotationAngle = _minRotationAngle;
        }

        transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);
    }
    #endregion

    #region Context Menus
    [ContextMenu("Add Weight")]
    private void EditorAddWeight()
    {
        AddWeight(10f);
    }

    [ContextMenu("Remove Weight")]
    private void EditorRemoveWeight()
    {
        AddWeight(-10f);
    }

    [ContextMenu("Reset")]
    private void EditorReset()
    {
        Reset();
    }
    #endregion
}
