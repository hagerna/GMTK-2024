using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "Shape", order = 1)]
public class ShapeSO : ScriptableObject
{
    // Producer
    public ProductionType prodType;
    public float prodMultiplier, lockInMultiplier, prodDelay = 1f;
    // + Modifier List

    // Scaler
    public float width, height = 1f;
    public float horizontalGrowth, verticalGrowth;
    public ScalerType scalerType;
    public float rotation;
    // + Modifier list
}

public enum ScalerType
{
    Standard = 0,
    Smart = 1,
    Directional = 2
}
