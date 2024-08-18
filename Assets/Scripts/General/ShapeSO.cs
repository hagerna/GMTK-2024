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
    public Scaler scalerPrefab;
    // + Modifier list

    public static ShapeSO Copy(ShapeSO other)
    {
        ShapeSO copy = CreateInstance<ShapeSO>();
        copy.prodType = other.prodType;
        copy.prodMultiplier = other.prodMultiplier;
        copy.lockInMultiplier = other.lockInMultiplier;
        copy.prodDelay = other.prodDelay;
        copy.width = other.width;
        copy.height = other.height;
        copy.horizontalGrowth = other.horizontalGrowth;
        copy.verticalGrowth = other.verticalGrowth;
        copy.scalerType = other.scalerType;
        copy.rotation = other.rotation;
        copy.scalerPrefab = other.scalerPrefab;
        return copy;
    }
}

public enum ScalerType
{
    Standard = 0,
    Smart = 1,
    Directional = 2
}
