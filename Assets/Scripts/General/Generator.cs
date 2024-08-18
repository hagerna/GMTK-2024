using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Generator: MonoBehaviour
{
    [SerializeField]
    ShapeSO[] StarterShapes;
    [SerializeField]
    ShapeSO[] CurrencyShapes;

    public ShapeSO[] GetStartingShapes()
    {
        return new ShapeSO[] { StarterShapes[0], StarterShapes[1], StarterShapes[2], StarterShapes[3], CurrencyShapes[0] };
    }
}
